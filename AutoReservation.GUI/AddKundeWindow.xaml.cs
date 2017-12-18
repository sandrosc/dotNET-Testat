﻿using System;
using System.Windows;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.GUI
{
    public partial class AddKundeWindow : Window
    {
        public AddKundeWindow()
        {
            InitializeComponent();
        }

        private void AddKunde_Click(object sender, RoutedEventArgs e)
        {
            if (Nachname.Text == "" || Vorname.Text == "" || !Geburtstagsdatum.SelectedDate.HasValue)
            {
                MessageBox.Show("Nicht alle Daten ausgefüllt!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var kunde = new KundeDto()
                {
                    Geburtsdatum = Geburtstagsdatum.SelectedDate ?? DateTime.Now,
                    Nachname = Nachname.Text,
                    Vorname = Vorname.Text
                };
            }
        }
    }
}