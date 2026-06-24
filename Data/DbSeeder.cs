using System;
using System.Linq;
using ElectronicsStore.API.Models;

namespace ElectronicsStore.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var requiredCategories = new[]
            {
                new Category { Name = "Microcontrollers", Description = "Core microcontroller boards for embedded projects." },
                new Category { Name = "Sensors", Description = "Sensor modules for environmental and motion sensing." },
                new Category { Name = "Modules", Description = "Communication, power, and interface modules." },
                new Category { Name = "Tools", Description = "Tools and accessories for electronics assembly." },
                new Category { Name = "Arduino Boards", Description = "Arduino microcontroller boards." },
                new Category { Name = "Passive Components", Description = "Resistors, capacitors, and other passive parts." }
            };

            var existingCategoryNames = context.Categories.Select(c => c.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var categoriesToAdd = requiredCategories.Where(c => !existingCategoryNames.Contains(c.Name)).ToList();

            if (categoriesToAdd.Any())
            {
                await context.Categories.AddRangeAsync(categoriesToAdd);
                await context.SaveChangesAsync();
            }

            var categories = context.Categories.ToDictionary(c => c.Name, c => c, StringComparer.OrdinalIgnoreCase);

            var products = new[]
            {
                new Product
                {
                    Name = "Arduino Uno R3",
                    CategoryId = categories["Microcontrollers"].Id,
                    Price = 450.00m,
                    Quantity = 15,
                    Description = "Original Arduino Uno Rev3 with ATmega328P microcontroller.",
                    OperatingVoltage = "5V",
                    SafetyNote = "Ensure correct polarity when connecting power.",
                    DatasheetLink = "https://docs.arduino.cc/hardware/uno-rev3",
                    SampleCodeLink = "https://github.com/arduino/Arduino/blob/master/build/shared/examples/01.Basics/Blink/Blink.ino",
                    ImageUrl = "/uploads/products/arduino-uno.jpg"
                },
                new Product
                {
                    Name = "LM35 Temperature Sensor",
                    CategoryId = categories["Sensors"].Id,
                    Price = 35.00m,
                    Quantity = 20,
                    Description = "Precision centigrade temperature sensor.",
                    OperatingVoltage = "5V",
                    SafetyNote = "Outputs analog voltage proportional to temperature.",
                    DatasheetLink = "https://www.ti.com/lit/ds/symlink/lm35.pdf",
                    SampleCodeLink = "https://github.com/example/lm35-reading.ino",
                    ImageUrl = "/uploads/products/lm35.jpg"
                },
                new Product
                {
                    Name = "Servo Motor SG90",
                    CategoryId = categories["Modules"].Id,
                    Price = 120.00m,
                    Quantity = 8,
                    Description = "Micro servo motor for robotics projects.",
                    OperatingVoltage = "5V",
                    SafetyNote = "Do not exceed operating voltage, may damage motor.",
                    DatasheetLink = "https://www.electronicoscaldas.com/datasheet/SG90.pdf",
                    SampleCodeLink = "https://github.com/example/servo-control.ino",
                    ImageUrl = "/uploads/products/sg90.jpg"
                },
                new Product
                {
                    Name = "16x2 LCD Display",
                    CategoryId = categories["Modules"].Id,
                    Price = 85.00m,
                    Quantity = 12,
                    Description = "Character LCD display with I2C adapter.",
                    OperatingVoltage = "5V",
                    SafetyNote = "Use I2C adapter for easier wiring.",
                    DatasheetLink = "https://www.datasheet.com/lcd-16x2",
                    SampleCodeLink = "https://github.com/example/lcd-i2c.ino",
                    ImageUrl = "/uploads/products/lcd16x2.jpg"
                },
                new Product
                {
                    Name = "ESP8266 WiFi Module",
                    CategoryId = categories["Modules"].Id,
                    Price = 180.00m,
                    Quantity = 5,
                    Description = "ESP8266-01 WiFi module for IoT projects.",
                    OperatingVoltage = "3.3V",
                    SafetyNote = "Use level shifter for 5V systems.",
                    DatasheetLink = "https://www.espressif.com/sites/default/files/documentation/esp8266-technical_reference_en.pdf",
                    SampleCodeLink = "https://github.com/example/esp8266-mqtt.ino",
                    ImageUrl = "/uploads/products/esp8266.jpg"
                },
                new Product
                {
                    Name = "9V Battery with Connector",
                    CategoryId = categories["Tools"].Id,
                    Price = 25.00m,
                    Quantity = 30,
                    Description = "Standard 9V battery with snap connector.",
                    OperatingVoltage = "9V",
                    SafetyNote = "Do not short circuit battery terminals.",
                    ImageUrl = "/uploads/products/9v-battery.jpg"
                },
                new Product
                {
                    Name = "Resistor Kit 1/4W (100pcs)",
                    CategoryId = categories["Passive Components"].Id,
                    Price = 65.00m,
                    Quantity = 10,
                    Description = "Assorted resistors from 10Ω to 1MΩ.",
                    OperatingVoltage = "N/A",
                    SafetyNote = "Check resistance before soldering.",
                    ImageUrl = "/uploads/products/resistor-kit.jpg"
                },
                new Product
                {
                    Name = "Breadboard 830 Points",
                    CategoryId = categories["Tools"].Id,
                    Price = 95.00m,
                    Quantity = 7,
                    Description = "Full-size breadboard for prototyping.",
                    OperatingVoltage = "N/A",
                    SafetyNote = "Handle with care to avoid bending pins.",
                    ImageUrl = "/uploads/products/breadboard.jpg"
                },
                new Product
                {
                    Name = "HC-SR04 Ultrasonic Sensor",
                    CategoryId = categories["Sensors"].Id,
                    Price = 55.00m,
                    Quantity = 3,
                    Description = "Ultrasonic distance measurement sensor.",
                    OperatingVoltage = "5V",
                    SafetyNote = "Keep sensor away from water.",
                    DatasheetLink = "https://www.electrokit.com/uploads/productfile/41015/HCSR04.pdf",
                    SampleCodeLink = "https://github.com/example/ultrasonic-distance.ino",
                    ImageUrl = "/uploads/products/hc-sr04.jpg"
                },
                new Product
                {
                    Name = "IRF540 MOSFET",
                    CategoryId = categories["Passive Components"].Id,
                    Price = 30.00m,
                    Quantity = 0,
                    Description = "N-Channel MOSFET for power switching.",
                    OperatingVoltage = "Up to 100V",
                    SafetyNote = "Use heatsink at high currents.",
                    DatasheetLink = "https://www.irf.com/product-info/datasheets/data/irf540.pdf",
                    ImageUrl = "/uploads/products/irf540.jpg"
                }
            };

            foreach (var product in products)
            {
                if (!context.Products.Any(p => p.Name == product.Name))
                {
                    await context.Products.AddAsync(product);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}