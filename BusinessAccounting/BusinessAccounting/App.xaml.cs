﻿using BusinessAccounting.Model;
using BusinessAccounting.Model.Entity;
using BusinessAccounting.View;
using log4net;
using System;
using System.Threading;
using System.Windows;

namespace BusinessAccounting
{
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            SetLanguageDictionary();
            InitDatabase();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(Application_UnhandledExceptionEventHandler);
        }

        private void Application_UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            DisplayExceptionToUser(e);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            DisplayExceptionToUser(e.Exception);
            e.Handled = true;
        }

        private void DisplayExceptionToUser(Exception pException)
        {
            Log.Error(pException.Message, pException);
            new DialogBox(null, this.FindResource("AppExceptionMessage").ToString(), DialogBoxType.Error, pException).ShowDialog();
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("Resources\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
                case "ru-RU":
                    dict.Source = new Uri("Resources\\StringResources.ru-RU.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("Resources\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
            }
            Resources.MergedDictionaries.Add(dict);
        }

        private void InitDatabase()
        {
            var context = new DatabaseContext();

            // TODO: Remove next lines
            foreach (var ContactPosition in context.Set<JobTitle>())
            {
                //MessageBox.Show(ContactPosition.Name);
            }
        }
    }
}