
namespace WrtWebSocketServer.Models
{
    public class Report
    {
        private static readonly Dictionary<string, List<string>> ValidRoutes = new()
        {
            { nameof(Constants.Routes.AlgerElAffroun), new List<string>(Constants.Routes.AlgerElAffroun.AllGares) },
            { nameof(Constants.Routes.AlgerThenia), new List<string>(Constants.Routes.AlgerThenia.AllGares) },
            { nameof(Constants.Routes.AlgerOuedAissi), new List<string>(Constants.Routes.AlgerOuedAissi.AllGares) },
            { nameof(Constants.Routes.AghaAeroport), new List<string>(Constants.Routes.AghaAeroport.AllGares) }
        };

        private string _trainRoute;
        private string _currentGare;
        private string _destinationGare;




        public string TrainRoute
        {
            get => _trainRoute;
            set
            {
                if (!ValidRoutes.ContainsKey(value))
                {
                    throw new ArgumentException($"Invalid : {value}");
                }
                _trainRoute = value;
            }
        }
        public string DestinationtGare
        {
            get => _destinationGare;
            set
            {
           

              if (!ValidRoutes[TrainRoute].Contains(value))
                {
                    throw new ArgumentException($"Invalid: {value}");
                }
                _destinationGare = value;
            }
        }

        public string CurrentGare
        {
            get => _currentGare;
            set
            {
               

                if (!ValidRoutes[TrainRoute].Contains(value))
                {
                    throw new ArgumentException($"Invalid: {value} ");
                }
                _currentGare = value;
            }
        }

        public DateTime ArrivalHour{ get; set; }
        
    }
}

