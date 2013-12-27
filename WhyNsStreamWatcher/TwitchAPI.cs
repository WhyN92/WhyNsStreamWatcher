using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    class TwitchAPI
    {


        private string StreamClientName = "Twitch.tv";

        // The API URL
        private string baseApiUrl = "https://api.twitch.tv/kraken/";

        // Twitch Username, Used in API URL
        private string TwitchUsername;

        // Holds all the Channels
        List<Channel> Channels = new List<Channel>();


        public TwitchAPI(string TwitchUsername) {

            this.TwitchUsername = TwitchUsername;
        



        }


        // Twitch API Comment:
        // Returns a list of follows objects.
        public List<Channel> GetChannelsUserIsFollowing()
        {

            string hostURI = baseApiUrl + "users/" + this.TwitchUsername + "/follows/channels";

            JObject FollowingChannels = getData(hostURI);

            if (FollowingChannels != null)
            {

                foreach (JObject fc in FollowingChannels["follows"])
                {

                    JObject c = (JObject)fc["channel"];

                    Channel newChannel = new Channel();

                    newChannel.StreamClientName = this.StreamClientName;

                    newChannel.display_name = (string)c["display_name"];
                    newChannel.name = (string)c["name"];
                    newChannel.status = (string)c["status"];
                    newChannel.game = (string)c["game"];
                    newChannel.logo = (string)c["logo"];
                    newChannel.url = (string)c["url"];

                    newChannel.id = (int)c["_id"];
                    newChannel.views = (int)c["views"];

                    newChannel.updated_at = (DateTime)c["updated_at"];

                    // Check if stream is live
                    JObject StreamChannel = GetStreamChannelData((string)c["name"]);

                    if (StreamChannel != null)
                    {
                        if (StreamChannel["stream"].HasValues)
                        {
                            // Channel is Live

                            JObject cLive = (JObject)StreamChannel["stream"];

                            newChannel.live = true;

                            newChannel.gameLive = (string)cLive["game"];
                            newChannel.viewersLive = (int)cLive["viewers"];


                        }
                        else
                        {
                            newChannel.live = false;

                        }


                        Channels.Add(newChannel);

                    }
                    else { return null; }


                }


                return this.Channels;
            }

            return null;

        } // End of GetChannelsUserIsFollowing()




        // Twitch API Comment:
        // Returns a stream object if live.
        private JObject GetStreamChannelData(string channelName)
        {

            string hostURI = baseApiUrl + "streams/" + channelName;

            JObject data = getData(hostURI);

            return data;

        }



        // Getting data based on URL
        private JObject getData(string url)
        {

            string hostURI = url;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(hostURI);
            request.Method = "GET";
            request.Accept = "application/vnd.twitchtv.v3+json";
            //request.Headers.Add();

            String jsonString = String.Empty;

            JObject jObj = null;

            try { 

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    jsonString = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }

                jObj = JObject.Parse(jsonString);

            }catch{
            
               
            
            }

            

            return jObj;

        }

    }
}
