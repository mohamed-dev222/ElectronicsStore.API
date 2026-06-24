using AutoMapper;
using ElectronicsStore.API.DTOs.UnavailableProduct;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Services.Interfaces;
using ElectronicsStore.API.UnitOfWork;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.API.Services.Implementations
{
    public class UnavailableProductService : IUnavailableProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UnavailableProductService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<UnavailableRequestResponseDto> CreateAsync(CreateUnavailableRequestFormDto dto)
        {
            var request = _mapper.Map<UnavailableProductRequest>(dto);
            request.ImageUrl = await UploadImageAsync(dto.ImageFile);

            await _unitOfWork.UnavailableProductRequests.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UnavailableRequestResponseDto>(request);
        }

        public async Task<PaginationResult<UnavailableRequestResponseDto>> GetAllAsync(int page, int pageSize, bool? isFulfilled)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var requests = await _unitOfWork.UnavailableProductRequests.GetAllAsync(page, pageSize, isFulfilled);
            var totalCount = await _unitOfWork.UnavailableProductRequests.Query()
                .Where(r => !isFulfilled.HasValue || r.IsFulfilled == isFulfilled.Value)
                .CountAsync();

            return new PaginationResult<UnavailableRequestResponseDto>
            {
                Items = _mapper.Map<IEnumerable<UnavailableRequestResponseDto>>(requests),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<UnavailableRequestResponseDto> MarkAsFulfilledAsync(int id)
        {
            var request = await _unitOfWork.UnavailableProductRequests.GetByIdAsync(id);
            if (request == null)
                throw new NotFoundException("Unavailable product request not found.");

            request.IsFulfilled = true;
            _unitOfWork.UnavailableProductRequests.Update(request);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UnavailableRequestResponseDto>(request);
        }

        private async Task<string?> UploadImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                throw new ValidationException("File format not supported.");
            }

            if (file.Length > 5 * 1024 * 1024)
            {
                throw new ValidationException("File size must be 5MB or less.");
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads", "unavailable-products");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(uploadsFolder, fileName);

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/unavailable-products/{fileName}";
        }
    }
}