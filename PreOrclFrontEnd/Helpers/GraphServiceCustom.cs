using Microsoft.Graph;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Helpers
{
    public class GraphServiceCustom
    {
        public async Task<List<ResultsItem>> GetMyCalendarView(GraphServiceClient graphClient)
        {
            List<ResultsItem> items = new List<ResultsItem>();

            // Define the time span for the calendar view.
            List<QueryOption> options = new List<QueryOption>();
            options.Add(new QueryOption("startDateTime", DateTime.Now.ToString("o")));
            options.Add(new QueryOption("endDateTime", DateTime.Now.AddDays(7).ToString("o")));

            ICalendarCalendarViewCollectionPage calendar = await graphClient.Me.Calendar.CalendarView.Request(options).GetAsync();

            if (calendar?.Count > 0)
            {
                foreach (Event current in calendar)
                {
                    items.Add(new ResultsItem
                    {
                        Display = current.Subject,
                        Id = current.Id
                    });
                }
            }
            return items;
        }
    }

    public class ResultsItem
    {
        public string Display { get; set; }
        public string Id { get; set; }
    }
}
