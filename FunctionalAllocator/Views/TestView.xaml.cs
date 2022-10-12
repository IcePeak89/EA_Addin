using FunctionalAllocator.ViewModels;

namespace FunctionalAllocator.Views
{
    public partial class TestView
    {
        public TestView(TestViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}