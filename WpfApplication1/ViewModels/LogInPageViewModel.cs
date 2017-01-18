using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1.ViewModels
{
    public class LogInPageViewModel : ReactiveObject
    {
        string userName = "grokys";
        string password;

        public LogInPageViewModel()
        {
            LogIn = ReactiveCommand.Create(
                DoLogIn,
                this.WhenAnyValue(
                    x => x.UserName,
                    x => x.Password,
                    (username, password) =>
                        !string.IsNullOrWhiteSpace(username) &&
                        !string.IsNullOrWhiteSpace(password)));
        }

        public string UserName
        {
            get { return userName; }
            set { this.RaiseAndSetIfChanged(ref userName, value); }
        }

        public string Password
        {
            get { return password; }
            set { this.RaiseAndSetIfChanged(ref password, value); }
        }

        public ReactiveCommand<Unit, Unit> LogIn { get; }

        private void DoLogIn()
        {

        }
    }
}
