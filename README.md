Remote-Users
============
Current Issues

1. Need to figure out how to refresh the Data Grid Control when calling 
    a. RemoteSessionLogOff(((SessionViewModel)gridUserSessionInfo.SelectedItem).Server, ((SessionViewModel)gridUserSessionInfo.SelectedItem).SessionID);
    b. RemoteSessionDisconnect(((SessionViewModel)gridUserSessionInfo.SelectedItem).Server, ((SessionViewModel)gridUserSessionInfo.SelectedItem).SessionID);
   
2. Getting Cross thread errors unless this is added
    DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
3. Determine a better way to do tis

/// <summary>
            /// Creates the session source.
            /// </summary>
            /// <returns></returns>
            protected ObservableCollection<SessionViewModel> CreateSessionSource() {
                ObservableCollection<SessionViewModel> sess = new ObservableCollection<SessionViewModel>();
                ITerminalServicesManager manager = new TerminalServicesManager();
                var t = Task.Factory.StartNew(() => Parallel.ForEach(ServerList.Servers, ServerName => {
                    ITerminalServer server = manager.GetRemoteServer(ServerName);
                    try {
                        try {
                            server.Open();
                            Parallel.ForEach(server.GetSessions(), session => {
                                var t1 = Task.Factory.StartNew(() => sess.Add(new SessionViewModel() { Server = server.ServerName, Domain = session.DomainName.ToUpper(), User = session.UserName, SessionID = session.SessionId, State = session.ConnectionState, IP = session.ClientIPAddress, Workstation = session.WindowStationName, Connect = session.ConnectTime, Login = session.LoginTime, Idle = session.IdleTime }));
                                t1.Wait();
                            });
                            ;
                        }
                        catch (InvalidOperationException i) {
                            DXMessageBox.Show(String.Format("{0}\\n{1}", i.Message, i.StackTrace));
                        }
                        catch (UnauthorizedAccessException a) {
                            DXMessageBox.Show(String.Format("{0}\\n{1}", a.Message, a.StackTrace));
                        }
                        catch (Win32Exception w) {
                            DXMessageBox.Show(String.Format("{0}\\n{1}", w.Message, w.StackTrace));
                        }
                        catch (SystemException s) {
                            DXMessageBox.Show(String.Format("{0}\\n{1}", s.Message, s.StackTrace));
                        }
                        catch (Exception e) {
                            DXMessageBox.Show(String.Format("{0}\\n{1}", e.Message, e.StackTrace));
                        }
                    }
                    catch (InvalidOperationException i) {
                        DXMessageBox.Show(String.Format("{0}\\n{1}", i.Message, i.StackTrace));
                    }
                    catch (UnauthorizedAccessException a) {
                        DXMessageBox.Show(String.Format("{0}\\n{1}", a.Message, a.StackTrace));
                    }
                    catch (Win32Exception w) {
                        DXMessageBox.Show(String.Format("{0}\\n{1}", w.Message, w.StackTrace));
                    }
                    catch (SystemException s) {
                        DXMessageBox.Show(String.Format("{0}\\n{1}", s.Message, s.StackTrace));
                    }
                    catch (Exception e) {
                        DXMessageBox.Show(String.Format("{0}\\n{1}", e.Message, e.StackTrace));
                    }
                    finally {
                        if (server != null)
                            ((IDisposable)server).Dispose();
                    }
                })); 
                return sess;
            }
4. Determine better way to do this
/// <summary>
        /// Handles the SelectionChanged event of the tbServerFileName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbServerFileName_SelectionChanged(object sender, RoutedEventArgs e) {
            ServerList.Servers.Clear();
            if (tbServerFileName.Text.Trim().Length > 0) {
                string[] lines = File.ReadAllLines(tbServerFileName.Text);
                foreach (string line in lines) {
                    ServerList.Servers.Add(line);
                }
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            DataContext = new SessionViewModel.SessionSource();
            tableView.BestFitColumns();
        }
