using articleapp.Models;
using articleapp.Repo;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using articleapp.Interfaces;

namespace articleapp.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly ArticleRepo _articleRepo;

        [ObservableProperty]
        private string searchQuery = string.Empty;

        [ObservableProperty]
        private ObservableCollection<BasicArticleWithDetails> articles;

        [ObservableProperty]
        private bool isSearching;

        public SearchViewModel(ArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
            Articles = new ObservableCollection<BasicArticleWithDetails>();
        }

        partial void OnSearchQueryChanged(string value)
        {
            if (!IsSearching)
            {
                SearchArticlesCommand.Execute(null);
            }
        }

        [RelayCommand]
        private async Task SearchArticles()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Articles.Clear();
                return;
            }

            try
            {
                IsSearching = true;
                var searchResults = await _articleRepo.SearchArticle(SearchQuery);

                Articles.Clear();
                foreach (var article in searchResults)
                {
                    Articles.Add(article);
                }
            }
            finally
            {
                IsSearching = false;
            }
        }
    }
}