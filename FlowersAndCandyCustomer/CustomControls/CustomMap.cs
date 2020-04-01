using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace FlowersAndCandyCustomer.CustomControls
{
    public class CustomMap : Map
    {
        public event EventHandler<MapTapEventArgs> Tapped;
        public List<CustomPin> CustomPins { get; set; }
        public List<Position> RouteCoordinates { get; set; }

        public CustomMap()
        {
            RouteCoordinates = new List<Position>();
        }
        public CustomMap(MapSpan region)
           : base(region)
        {

        }
        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tapped;

            if (handler != null)
                handler(this, e);
        }
    }
    public class CustomPin
    {
        public Pin Pin { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public bool startPin = false;
    }

    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
    //rahul
   
}
