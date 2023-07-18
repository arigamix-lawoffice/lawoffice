using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.EDS;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.EDS
{
    public sealed class SignatureSettingsUIExtension : CardUIExtension
    {
        public override Task Initialized(ICardUIExtensionContext context)
        {
            var control = (GridViewModel) context.Model.Controls[SignatureHelper.SignatureSettingsEncryptionDigestControlName];
            control.RowInitializing += RowInitializing;
            //control.RowEditorClosing += RowClosing;

            return Task.CompletedTask;
        }

        private static async void RowInitializing(object sender, GridRowEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                if (e.Row is not null)
                {
                    e.RowModel.Controls["DigestAlgorithm"].IsReadOnly = (e.Row.Fields[SignatureHelper.SignatureSettingsDigestAlgorithmsIDName]) == null;
                    e.Row.FieldChanged += (s, ev) =>
                    {
                        if (s is not CardRow row)
                        {
                            return;
                        }

                        switch (ev.FieldName)
                        {
                            case SignatureHelper.SignatureSettingsEncryptionAlgorithmIDName:
                                row.Fields[SignatureHelper.SignatureSettingsDigestAlgorithmsIDName] = null;
                                row.Fields[SignatureHelper.SignatureSettingsDigestAlgorithmsNameName] = null;
                                row.Fields[SignatureHelper.SignatureSettingsDigestAlgorithmsOIDName] = null;
                                e.RowModel.Controls["DigestAlgorithm"].IsReadOnly = ev.FieldValue == null;
                                break;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                deferral.SetException(ex);
            }
            finally
            {
                deferral.Dispose();
            }
        }
    }
}
