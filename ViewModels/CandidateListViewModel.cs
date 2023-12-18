using AutoMapper.Execution;
using Batsy.Infrastructures;
using Batsy.Models;
using Batsy.Resources.Interfaces;
using Batsy.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Batsy.ViewModels
{
    public class CandidateListViewModel:ViewModel
    {
        private INavigationService _navigation;
        private ObservableCollection<CandidateModel> _candidates;// = new ObservableCollection<Candidate>();

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        public RelayCommand NavigateToDashbordCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand<object> EditCommand
        {
            get
            {
                return new RelayCommand<object>(async (object o) =>  await EditRecord(o) );
            }
        }

        public RelayCommand<object> DeleteCommand
        {
            get
            {
                return new RelayCommand<object>(async (object o) => await DeleteRecord(o));
            }
        }

        public ObservableCollection<CandidateModel> Candidates
        {
            get => _candidates ??= new ObservableCollection<CandidateModel>();
            set
            {
                SetProperty(ref _candidates, value);
                OnPropertyChanged(nameof(Candidates));
            }
        }
        private int _records;
        public int Records
        {
            get=> _records;
            set
            {
                SetProperty(ref _records, value);
                OnPropertyChanged(nameof(Records));
                ShowRecords = $"No. of records: {Records}";
            }
        }

        private string _showRecords;
        public string ShowRecords
        {
            get => _showRecords;
            set
            {
                SetProperty(ref _showRecords, value);
                OnPropertyChanged(nameof(ShowRecords));
               
            }
        }

        public CandidateListViewModel(INavigationService navServie)
        {
            _navigation = navServie;
            Task.Run(async () =>
            {
                await Fetchrecords();
            });
            
        }

        private async Task Fetchrecords()
        {
            Candidates =
            [
                new CandidateModel() {Reg_no="00000", Cand_name="Candidat 1", Cert_sn="1234678", D_of_b="12/23/2033", Sex="M", S_of_o="Abia"},
                new CandidateModel() { Reg_no = "00000", Cand_name = "Candidate 2", Cert_sn = "1234678", D_of_b = "12/23/2033", Sex = "M", S_of_o = "Abia" }
            ];
            Records = Candidates.Count;
            await Task.WhenAll();
        }

        private async Task EditRecord(object e)
        {
            if(e == null)
            {
                MessageBox.Show("No records selected", "Edit record", MessageBoxButton.OK, MessageBoxImage.Error);
                await Task.CompletedTask;
                return;
            }
            CandidateModel? _cands = e as CandidateModel;

            MessageBox.Show(_cands?.Cand_name);
            await Task.CompletedTask;
        }

        private async Task DeleteRecord(object e)
        {
            if (e == null)
            {
                MessageBox.Show("No records selected", "Delete record", MessageBoxButton.OK, MessageBoxImage.Error);
                await Task.CompletedTask;
                return;
            }
            CandidateModel? _cands = e as CandidateModel;

            if(MessageBox.Show($"Confirm delete {_cands?.Cand_name}","Delete record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                await Task.CompletedTask;
                return;
            }
            
        }
    }
}
