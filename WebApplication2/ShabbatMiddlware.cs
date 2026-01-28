namespace ClinicAPI
{
    public class ShabbatMiddleware
    {
        private readonly RequestDelegate _next;

        public ShabbatMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var now = DateTime.Now;
            var day = now.DayOfWeek;
            var hour = now.Hour;

            bool isShabbatTime = false;

            
            if (day == DayOfWeek.Friday && hour >= 16)
            {
                isShabbatTime = true;
            }
            // בדיקה ליום שבת - כל היום עד שעה 19:00
            else if (day == DayOfWeek.Saturday && hour < 19)
            {
                isShabbatTime = true;
            }

            if (isShabbatTime)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                context.Response.ContentType = "application/json; charset=utf-8";

                var response = new
                {
                    message = "האתר סגור זמנית לקבלת שבת",
                    reason = "שָׁמוֹר וְזָכוֹר בְּדִבּוּר אֶחָד 🕯️🕯️",
                    status = "Shabbat Mode"
                };

                await context.Response.WriteAsJsonAsync(response);
                return;
            }

            await _next(context);
        }
    }
}

