using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

using EA;

using FunctionalAllocator.Commands;
using FunctionalAllocator.Profile;

using Microsoft.Win32;

//using Newtonsoft.Json;

namespace FunctionalAllocator.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        public bool IsWindowOpen = false;

        private readonly Repository _repository;

        public TestViewModel(Repository repository)
        {
            _repository = repository;
        }
    }

}