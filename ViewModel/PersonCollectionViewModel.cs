﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Comandos.ViewModel
{
    public class PersonCollectionViewModel : BaseViewModel
    {
        PersonViewModel personEdit;
        bool isEditing;

        public bool IsEditing
        {
            private set { SetProperty(ref isEditing, value); }
            get { return isEditing; }
        }

        public PersonViewModel PersonEdit
        {
            set { SetProperty(ref personEdit, value); }
            get { return personEdit; }
        }

        public ICommand NewCommand { private set; get; }
        public ICommand SubmitCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }

        public IList<PersonViewModel> Persons { get; } = new ObservableCollection<PersonViewModel>();

        public PersonCollectionViewModel()
        {
            NewCommand = new Command(
                execute: () =>
                {
                    PersonEdit = new PersonViewModel();
                    PersonEdit.PropertyChanged += OnPersonEditPropertyChanged;
                    IsEditing = true;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !IsEditing;
                });

            SubmitCommand = new Command(
           execute: () =>
           {
               Persons.Add(PersonEdit);
               PersonEdit.PropertyChanged -= OnPersonEditPropertyChanged;
               PersonEdit = null;
               IsEditing = false;
               RefreshCanExecutes();
           },
           canExecute: () =>
           {
               return PersonEdit != null &&
                      PersonEdit.Name != null &&
                      PersonEdit.Name.Length > 1 &&
                      PersonEdit.Age > 0;
           });

            CancelCommand = new Command(
           execute: () =>
           {
               PersonEdit.PropertyChanged -= OnPersonEditPropertyChanged;
               PersonEdit = null;
               IsEditing = false;
               RefreshCanExecutes();
           },
           canExecute: () =>
           {
               return IsEditing;
           });

            void OnPersonEditPropertyChanged(object sender, PropertyChangedEventArgs args)
            {
                (SubmitCommand as Command).ChangeCanExecute();
            }

            void RefreshCanExecutes()
            {
                (NewCommand as Command).ChangeCanExecute();
                (SubmitCommand as Command).ChangeCanExecute();
                (CancelCommand as Command).ChangeCanExecute();
            }
        }
    }
}
