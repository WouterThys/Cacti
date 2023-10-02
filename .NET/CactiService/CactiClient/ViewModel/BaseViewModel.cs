using DevExpress.Dialogs.Core.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;

namespace CactiClient.ViewModel
{
    [POCOViewModel]
    public abstract class BaseViewModel
    {
        public virtual Guid Guid { get; } = Guid.NewGuid();
        public abstract string Name { get; }
        public abstract string Title { get; }
        public abstract string ViewName { get; }


        public virtual bool IsLoading { get; set; }

        public virtual IMessageBoxService MessageBoxService { get { throw new NotImplementedException(); } }
        public virtual IDialogService DialogService { get { throw new NotImplementedException(); } }
        public virtual IDispatcherService DispatcherService { get { throw new NotImplementedException(); } }

        public virtual IDocumentManagerService DocumentManagerService { get { throw new NotImplementedException(); } }

        [ServiceProperty(Key = "FloatingDocumentService")]
        protected virtual IDocumentManagerService FloatingDocumentManagerService { get { throw new NotImplementedException(); } }


        public abstract void Load();

        public virtual void UpdateCommands() 
        {
        
        }

        #region Documents
        public IDocument? ShowDocument(BaseViewModel? viewModel)
        {
            if (viewModel == null) return null;
            if (DocumentManagerService == null) return null;

            IDocument document = DocumentManagerService.FindDocumentByIdOrCreate(viewModel.Guid, x => CreateDocument(viewModel, DocumentManagerService));
            document.Show();
            return document;
        }

        protected IDocument CreateDocument(BaseViewModel viewModel, IDocumentManagerService service)
        {
            IDocument document = service.CreateDocument(viewModel.ViewName, viewModel);
            string title = viewModel.Title;
            if (!string.IsNullOrEmpty(viewModel.Title))
            {
                title = " " + viewModel.Title;
            }
            if (!string.IsNullOrEmpty(title))
            {
                document.Title = title;
            }
            document.DestroyOnClose = true;
            return document;
        }

        #endregion

    }
}
