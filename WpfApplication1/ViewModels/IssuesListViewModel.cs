using Octokit;
using Octokit.Reactive;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ViewModels
{
    public class IssuesListViewModel : ReactiveObject
    {
        public ReactiveList<string> Items { get; } = new ReactiveList<string>();

        public void Load(string userName, string password)
        {
            var connection = new Connection(new ProductHeaderValue("TestApp", "0.0.1"));
            //connection.Credentials = new Credentials(userName, password);

            var client = new GitHubClient(connection);
            var rxClient = new ObservableGitHubClient(client);

            //rxClient.Repository.GetAllForUser(userName)
            //    .SelectMany(x => rxClient.Issue.GetAllForRepository(x.Id))
            //    .ObserveOn(RxApp.MainThreadScheduler)
            //    .Subscribe(x => Items.Add(x.Title));

            //var user = rxClient.User.Get(userName).Wait();

            //rxClient.User.Get(userName).SelectMany(user =>
            //{
            //    return rxClient.Issue.GetAllForRepository("GitHub", "VisualStudio")
            //        .Where(i => i.Assignee?.Id == user.Id);
            //}).ObserveOn(RxApp.MainThreadScheduler)
            //  .Subscribe(x => Items.Add(x.Title));


            rxClient.User.Get(userName)
                .Select(x => x.Id)
                .SelectMany(userId =>
                    rxClient.Issue.GetAllForRepository("GitHub", "VisualStudio")
                        .Where(x => x.Assignee?.Id == userId))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => Items.Add(x.Title));
        }
    }
}
