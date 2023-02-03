using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApp.DatabaseContext;
using TestApp.Domain;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentTimeController : ControllerBase
    {
        private readonly DateTimeDbContext _context;
        public CurrentTimeController(DateTimeDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("save-time")]
        public async Task<ActionResult> Post([FromBody] CurrentTime currentTime)
        {
            currentTime.CurrentTimeValue = DateTime.Now;

            _context.CurrentTime.Add(currentTime);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet] 
        [Route("get-time")]
        public async Task<ActionResult> Get()
        {
            var time = _context.CurrentTime.ToList().Last().CurrentTimeValue;

             var timeValue = time.Value.ToString("hh:mm:ss");

            var hour = time.Value.Hour;

            var minutes = time.Value.Minute;
            string value = "";

           var timeInWord = PrintWords(hour, minutes);
            //print time in words  
           
            string PrintWords(int hour, int minutes)
            {
                string[] nums = { "zero", "one", "two", "three", "four",
                            "five", "six", "seven", "eight", "nine",
                            "ten", "eleven", "twelve", "thirteen",
                            "fourteen", "fifteen", "sixteen", "seventeen",
                            "eighteen", "nineteen", "twenty", "twenty one",
                            "twenty two", "twenty three", "twenty four",
                            "twenty five", "twenty six", "twenty seven",
                            "twenty eight", "twenty nine",
                        };

                if (minutes == 0)
                   return value = nums[hour] + " o' clock ";

                else if (minutes > 0 && minutes <15)
                    return value = ""+minutes+" minute past " + nums[(hour % 12)];

                else if (minutes == 15)
                    return value = "quarter past " + nums[(hour % 12)];

                else if (minutes > 15 && minutes < 30)
                    return value = "" + minutes + " minute past " + nums[(hour % 12)];

                else if (minutes == 30)
                    return value = "half past " + nums[(hour % 12)];

                else if (minutes > 30 && minutes < 45 && minutes  !=45)
                    return value = nums[60 - minutes] + " minutes to " + nums[(hour % 12) + 1];

                else if (minutes == 45)
                    return value = "quarter to " + nums[(hour % 12) + 1];

                return value;
            }

            return Ok(timeValue + " --> "+ timeInWord);
        }

    }
}
