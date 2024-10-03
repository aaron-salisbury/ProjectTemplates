using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using System.Windows.Forms;
using WinXPApp.Base.MVP;
using WinXPApp.Views.SampleTools;

namespace WinXPApp.Presenters.SampleTools
{
    internal class UUIDGeneratorPresenter : Presenter
    {
        private readonly ISampleToolsService _sampleToolsService;

        private UUIDGeneratorView _view;

        public UUIDGeneratorPresenter(ISampleToolsService sampleToolsService)
        {
            _sampleToolsService = sampleToolsService;
        }

        internal override void Setup(UserControl view)
        {
            _view = (UUIDGeneratorView)view;

            _view.GenerateCommand += View_GenerateCommand;
        }

        internal override void Dismiss()
        {
            if (_view != null)
            {
                _view.GenerateCommand -= View_GenerateCommand;
            }
        }

        private void View_GenerateCommand(object sender, GenerateCommandEventArgs e)
        {
            if (_view != null)
            {
                string generatedUUID = _sampleToolsService.InitializeGUIDGeneration(e.ShouldCapitalize);

                _view.UUIDGenerated(generatedUUID);
            }
        }
    }
}
