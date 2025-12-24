using Scraping_Egy_Bus.Models;
using System.Collections.Concurrent;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Scraping_Egy_Bus.Scraping
{
    public class EgBusScraper
    {
        public static readonly Dictionary<int, string> CityNames = new Dictionary<int, string>
        {
            { 1, "القاهرة" }, { 2, "الاسكندرية" }, { 3, "مدينة برج العرب" }, { 4, "مدينة برج العرب الجديدة" },
            { 5, "الاسماعيلية" }, { 6, "التل الكبير" }, { 7, "فايد" }, { 8, "القنطرة شرق" }, { 9, "القنطرة غرب" },
            { 10, "ابوصير" }, { 11, "القصاصين" }, { 12, "اسوان" }, { 13, "ادفو" }, { 14, "كوم امبو" },
            { 15, "دراو" }, { 16, "نصرالنوبة" }, { 17, "اسيوط" }, { 18, "ديروط" }, { 19, "القوصية" },
            { 20, "ابنوب" }, { 21, "منفلوط" }, { 22, "اسيوط الجديدة" }, { 23, "ابوتيج" }, { 24, "ساحل سليم" },
            { 25, "البدارى" }, { 26, "صدفا" }, { 27, "الغنايم" }, { 28, "الاقصر" }, { 29, "الزينية" },
            { 30, "القرنة" }, { 31, "البياضية" }, { 32, "الطود" }, { 33, "ارمنت" }, { 34, "اسنا" },
            { 35, "طيبة الجديدة" }, { 36, "الغردقة" }, { 37, "القصير" }, { 38, "سفاجا" }, { 39, "مرسي علم" },
            { 40, "راس غارب" }, { 41, "حلايب" }, { 42, "الشلاتين" }, { 43, "دمنهور" }, { 44, "كفر الدوار" },
            { 45, "رشيد" }, { 46, "ادكو" }, { 47, "ابوالمطامير" }, { 48, "ابوحمص" }, { 49, "الدلنجات" },
            { 50, "المحمودية" }, { 51, "الرحمانية" }, { 52, "ايتاى البارود" }, { 53, "حوش عيسي" },
            { 54, "شبراخيت" }, { 55, "كوم حمادة" }, { 56, "بدر" }, { 57, "وادى النطرون" }, { 58, "النوبارية الجديدة" },
            { 59, "الواسطى" }, { 60, "بني سويف" }, { 61, "ناصر" }, { 62, "اهناسيا" }, { 63, "ببا" },
            { 64, "سمسطا" }, { 65, "بني سويف الجديدة" }, { 66, "الفشن" }, { 67, "بورسعيد" }, { 68, "ابورديس" },
            { 69, "ابوزنيمة" }, { 70, "نويبع" }, { 71, "طابا" }, { 72, "راس سدر" }, { 73, "دهب" },
            { 74, "شرم الشيخ" }, { 75, "سانت كاترين" }, { 76, "الطور" }, { 77, "الجيزة" }, { 78, "السادس من أكتوبر" },
            { 79, "الشيخ زايد" }, { 80, "الحوامدية" }, { 81, "البدرشين" }, { 82, "الصف" }, { 83, "اطفيح" },
            { 84, "العيّاط" }, { 85, "الباويطي" }, { 86, "منشأة القناطر" }, { 87, "اوسيم" }, { 88, "كرداسة" },
            { 89, "ابوالنمرس" }, { 90, "المنصورة" }, { 91, "طلخا" }, { 92, "ميت غمر" }, { 93, "دكرنس" },
            { 94, "اجا" }, { 95, "منية النصر" }, { 96, "السنبلاوين" }, { 97, "الكردى" }, { 98, "بنى عبيد" },
            { 99, "المنزلة" }, { 100, "تمى الامديد" }, { 101, "الجمالية" }, { 102, "شربين" }, { 103, "المطرية" },
            { 104, "بلقاس" }, { 106, "ميت سلسيل" }, { 107, "محلة دمنة" }, { 108, "جمصة" }, { 109, "نبروة" },
            { 110, "دمياط" }, { 111, "دمياط الجديدة" }, { 112, "راس البر" }, { 113, "فارسكور" }, { 114, "كفرسعد" },
            { 115, "الزرقا" }, { 116, "السرو" }, { 117, "الروضة" }, { 118, "كفر البطيخ" }, { 119, "عزبة البرج" },
            { 120, "ميت ابوغالب" }, { 121, "البلينا" }, { 122, "دار السلام" }, { 123, "ساقلته" }, { 124, "سوهاج" },
            { 125, "طما" }, { 126, "طهطا" }, { 127, "المراغة" }, { 128, "المنشاة" }, { 129, "اخميم" },
            { 130, "اخميم الجديدة" }, { 131, "جرجا" }, { 132, "جهينة الغربية" }, { 133, "سوهاج الجديدة" },
            { 135, "السويس" }, { 136, "الزقازيق" }, { 137, "بلبيس" }, { 138, "فاقوس" }, { 139, "كفر صقر" },
            { 140, "منيا القمح" }, { 141, "ابوكبير" }, { 142, "ابوحماد" }, { 143, "الحسينية" }, { 144, "ديرب نجم" },
            { 145, "الصالحية الجديدة" }, { 146, "العاشر من رمضان" }, { 148, "الشيخ زويد" }, { 149, "بئر العبد" },
            { 150, "نخل" }, { 151, "الحسنة" }, { 152, "رفح" }, { 153, "طنطا" }, { 154, "المحلة الكبرى" },
            { 155, "كفر الزيات" }, { 156, "زفتى" }, { 157, "السنطة" }, { 158, "قطور" }, { 159, "بسيون" },
            { 160, "سمنود" }, { 161, "الفيوم" }, { 162, "سنورس" }, { 163, "ابشواى" }, { 164, "اطسا" },
            { 165, "طامية" }, { 166, "يوسف الصديق" }, { 167, "بنها" }, { 168, "قليوب" }, { 169, "شبراالخيمة" },
            { 170, "القناطر الخيرية" }, { 171, "الخانكة" }, { 172, "كفرشكر" }, { 173, "طوخ" }, { 174, "قها" },
            { 175, "العبور" }, { 176, "الخصوص" }, { 177, "شبين القناطر" }, { 178, "ابوتشت" }, { 179, "فرشوط" },
            { 180, "نجع حمادى" }, { 181, "الوقف" }, { 182, "دشنا" }, { 183, "قنا" }, { 184, "قفط" },
            { 185, "قوص" }, { 187, "نقادة" }, { 188, "كفر الشيخ" }, { 189, "دسوق" }, { 190, "فوه" },
            { 191, "مطوبس" }, { 192, "بلطيم" }, { 193, "مصيف بلطيم" }, { 194, "الحامول" }, { 195, "بيلا" },
            { 196, "الرياض" }, { 197, "سيدى سالم" }, { 198, "قلين" }, { 199, "سيدى غازى" }, { 200, "برج البرلس" },
            { 201, "مسير" }, { 202, "مرسي مطروح" }, { 203, "الحمام" }, { 204, "العلمين" }, { 205, "الضبعة" },
            { 206, "النجيلة" }, { 207, "سيدى برانى" }, { 208, "السلوم" }, { 209, "سيوة" }, { 210, "الباجور" },
            { 211, "بركة السبع" }, { 212, "تلا" }, { 213, "السادات" }, { 214, "شبين الكوم" }, { 215, "الشهداء" },
            { 216, "قويسنا" }, { 217, "منوف" }, { 218, "بنى مزار" }, { 219, "دير مواس" }, { 220, "سمالوط" },
            { 221, "العدوة" }, { 222, "مطاى" }, { 223, "مغاغة" }, { 224, "ملوى" }, { 225, "المنيا" },
            { 226, "باريس" }, { 227, "بلاط" }, { 228, "الخارجة" }, { 229, "الفرافرة" }, { 230, "الداخلة" },
            { 231, "الواحات" }, { 232, "اتاى البارود" }, { 233, "اشمون" }, { 235, "الخازنداره" }, { 236, "الوادى" },
            { 237, "سبرباى" }, { 239, "جنوب سيناء" }, { 240, "شمال سيناء" }, { 241, "العريش" }, { 245, "ديرب نجم" },
            { 247, "جنيفه" }, { 248, "ابوصوير" }, { 249, "المثلث" }, { 251, "رمانه" }, { 252, "رابعه" },
            { 261, "مكتب الكفاح" }, { 262, "غرب الموهوب" }, { 263, "مكتب القصر" }, { 271, "الزعفرانه" },
            { 272, "شرق العوينات" }, { 273, "ابورماد" }, { 276, "ابو هريرة" }, { 277, "ابو منقار" },
            { 278, "سيدى عبد الرحمن" }, { 283, "ههيا" }, { 284, "الحسنيه" }, { 285, "البحر الاحمر" },
            { 286, "رأس حدربه" }, { 287, "حلوان" }, { 288, "أبو زعبل" }, { 290, "مسطرد" }, { 295, "التوفيقيه" },
            { 296, "مركز بدر" }, { 298, "الشرقية" }, { 303, "أبو سمبل" }, { 311, "الساحل الشمالي" },
            { 312, "اتاي البارود" }, { 317, "ابو قرقاص" }, { 318, "الاقصر غرب" }, { 322, "طرابلس" },
            { 323, "بنغازى" }, { 327, "مصراته" }, { 328, "سرت" }, { 329, "اجدابيا" }, { 332, "طبرق" },
        };

        private const string BaseUrl = "https://eg-bus.com";
        private static readonly HttpClient HttpClient;

        static EgBusScraper()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            HttpClient = new HttpClient(handler);
            HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        }

        public async Task<List<TempTrip>> ScrapeDaysAsync(DateTime startDate, int numberOfDays, CancellationToken cancellationToken = default)
        {
            var allTrips = new List<TempTrip>();

            for (int i = 0; i < numberOfDays; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Scraping for multiple days was cancelled.");
                    break;
                }
                var currentDate = startDate.AddDays(i);
                Console.WriteLine($"\n<<<<<<<<<< Scraping day {i + 1}/{numberOfDays} for date: {currentDate:yyyy-MM-dd} >>>>>>>>>>\n");

                var dailyTrips = await ScrapeAllTripsForDateAsync(currentDate, 25, cancellationToken);
                allTrips.AddRange(dailyTrips);
            }

            return allTrips.OrderBy(t => t.TripDate).ThenBy(t => t.FromCityName).ThenBy(t => t.ToCityName).ToList();
        }

        private async Task<List<TempTrip>> ScrapeAllTripsForDateAsync(DateTime date, int maxDegreeOfParallelism = 25, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Starting scraping for date: {date:yyyy-MM-dd} with {maxDegreeOfParallelism} parallel tasks.");
            var allTrips = new ConcurrentBag<TempTrip>();
            var cityIds = CityNames.Keys.ToList();

            var cityPairs = new List<(int from, int to)>();
            foreach (var fromId in cityIds)
            {
                foreach (var toId in cityIds)
                {
                    if (fromId != toId)
                    {
                        cityPairs.Add((fromId, toId));
                    }
                }
            }

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                CancellationToken = cancellationToken
            };

            await Parallel.ForEachAsync(cityPairs, options, async (pair, token) =>
            {
                try
                {
                    var fromCityName = CityNames.GetValueOrDefault(pair.from, $"ID {pair.from}");
                    var toCityName = CityNames.GetValueOrDefault(pair.to, $"ID {pair.to}");

                    Console.WriteLine($"-> Scraping: {fromCityName} to {toCityName}");

                    var foundTrips = await ScrapeRouteAsync(pair.from, pair.to, date);

                    if (foundTrips.Any())
                    {
                        foreach (var trip in foundTrips)
                        {
                            allTrips.Add(trip);
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"   [SUCCESS] Found {foundTrips.Count} trips for {fromCityName} -> {toCityName}.");
                        Console.ResetColor();
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("   [CANCELLED] Operation was cancelled.");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"   [ERROR] Failed to scrape route. Reason: {ex.Message}");
                    Console.ResetColor();
                }
            });

            return allTrips.OrderBy(t => t.FromCityName).ThenBy(t => t.ToCityName).ToList();
        }

        public async Task<List<TempTrip>> ScrapeRouteAsync(int fromCityId, int toCityId, DateTime date)
        {
            var tripsMap = new Dictionary<string, TempTrip>();
            var url = $"{BaseUrl}/master/tripcity?from_city_id={fromCityId}&to_city_id={toCityId}&trip_date={date:yyyy-MM-dd}";

            string htmlContent;
            try
            {
                htmlContent = await HttpClient.GetStringAsync(url);
            }
            catch (HttpRequestException)
            {
                return new List<TempTrip>();
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var tripNodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'geodir-category-content')]");

            if (tripNodes == null)
                return tripsMap.Values.ToList();

            foreach (var trip in tripNodes)
            {
                var priceNode = trip.SelectSingleNode(".//span[contains(text(),'جنيه')]");
                if (priceNode == null) continue;

                if (!decimal.TryParse(priceNode.InnerText.Replace("جنيه", "").Trim(), out var price)) continue;

                var codeMatch = Regex.Match(trip.InnerText, @"كود الرحلة\s*(\d+)");
                var tripCode = codeMatch.Success ? codeMatch.Groups[1].Value : null;
                if (!codeMatch.Success) continue;

                var timeMatch = Regex.Match(trip.InnerText, @"الساعة\s*(\d{1,2}:\d{2}\s*(?:صباحًا|صباحاً|مساءً))");
                var departureTime = timeMatch.Success ? timeMatch.Groups[1].Value : null;

                var bookingNode = trip.SelectSingleNode(".//a[contains(@class,'geodir-js-booking')]");
                string bookingUrl = null;
                if (bookingNode != null)
                {
                    var href = bookingNode.GetAttributeValue("href", "");
                    if (!string.IsNullOrWhiteSpace(href))
                    {
                        bookingUrl = WebUtility.HtmlDecode(href.StartsWith("http") ? href : BaseUrl + href);
                    }
                }

                var knownFeatures = new[] { "WiFi", "تكييف", "حمام", "فيديو", "شاشات عرض بالمقعد", "ضيافة" };
                var features = new List<string>();
                var tripText = trip.InnerText;
                foreach (var feature in knownFeatures)
                {
                    if (tripText.Contains(feature)) features.Add(feature);
                }
                var featuresText = features.Any() ? string.Join(", ", features) : null;

                CityNames.TryGetValue(fromCityId, out var fromCityName);
                CityNames.TryGetValue(toCityId, out var toCityName);
                fromCityName ??= $"ID {fromCityId}";
                toCityName ??= $"ID {toCityId}";

                var uniqueTripKey = $"{tripCode}-{departureTime}";

                if (!tripsMap.ContainsKey(uniqueTripKey))
                {
                    tripsMap[uniqueTripKey] = new TempTrip
                    {
                        FromCity = fromCityId,
                        ToCity = toCityId,
                        FromCityName = fromCityName,
                        ToCityName = toCityName,
                        TripDate = date.ToString("yyyy-MM-dd"),
                        CompanyName = "EG-BUS",
                        TripCode = tripCode,
                        Price = price,
                        DepartureTime = departureTime,
                        BookingUrl = bookingUrl,
                        Features = featuresText
                    };
                }
            }

            return tripsMap.Values.ToList();
        }
    }

}
