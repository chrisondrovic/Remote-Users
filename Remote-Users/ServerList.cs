using System;
using System.Collections.ObjectModel;

namespace Remote_Users {

    public static class ServerList {

        /// <summary>
        /// Sets the servers.
        /// </summary>
        /// <param name="servers">The servers.</param>
        internal static void SetServers(ObservableCollection<String> servers) {
            Servers = servers;
        }

        /// <summary>
        /// The servers
        /// </summary>
        public static ObservableCollection<String> Servers = new ObservableCollection<string>();
    }
}