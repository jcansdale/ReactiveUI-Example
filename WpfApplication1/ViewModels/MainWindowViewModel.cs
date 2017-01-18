using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        object currentPage;

        public MainWindowViewModel()
        {
            var login = new LogInPageViewModel();
            login.LogIn.Subscribe(_ =>
            {
                var issues = new IssuesListViewModel();
                issues.Load(login.UserName, login.Password);
                CurrentPage = issues;
            });
            currentPage = login;
        }

        public object CurrentPage
        {
            get { return currentPage; }
            private set { this.RaiseAndSetIfChanged(ref currentPage, value); }
        }
    }
}
