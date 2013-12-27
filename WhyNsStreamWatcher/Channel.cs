using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhyNsStreamWatcher
{
    public class Channel
    {


        // Stream Client Data

        public string 
            StreamClientName = string.Empty
        ;

        // Channel Data

        public string
            display_name,
            name,
            status,
            game,
            logo,
            url = string.Empty
        ;

        public int
            views,
            id = 0
        ;

        public DateTime
            updated_at
        ;

        // Live Data

        public string
            gameLive = string.Empty
        ;

        public int
            viewersLive = 0
        ;

        public bool
            live = false
        ;

        

        // Notification Data

        public bool
            IsLive,
            IsLiveNow,
            IsOfflineNow = false
        ;


        // Changed Data

        public int
            viewersLiveDifference = 0
        ;


    }
}
