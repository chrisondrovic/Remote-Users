Remote-Users
============
Current Issues

1. Need to figure out how to refresh the Data Grid Control when calling 
    a. RemoteSessionLogOff(((SessionViewModel)gridUserSessionInfo.SelectedItem).Server, ((SessionViewModel)gridUserSessionInfo.SelectedItem).SessionID);
    b. RemoteSessionDisconnect(((SessionViewModel)gridUserSessionInfo.SelectedItem).Server, ((SessionViewModel)gridUserSessionInfo.SelectedItem).SessionID);
   
2. Getting Cross thread errors unless this is added
    DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
3. Better way of doing code execution on tbServerFileName_SelectionChanged in MainWindow.xaml.cs (lines 129 - 142)
4. Better way of doing code execution on ObservableCollection<SessionViewModel> CreateSessionSource() in Session.cs (lines 322 - 373 )
