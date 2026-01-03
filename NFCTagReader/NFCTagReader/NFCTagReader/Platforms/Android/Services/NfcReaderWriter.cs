using NFCTagReader.Shared.Models;
using NFCTagReader.Shared.Services;
using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFCTagReader.Services;

public partial class NfcReaderWriter : INfcReaderWriter
{
#if ANDROID
    public NfcReaderWriter()
    {
        CrossNFC.Current.OnMessageReceived += OnMessageReceived;
        CrossNFC.Current.OnMessagePublished += OnMessagePublished;
        CrossNFC.Current.StartListening();
    }


    private void OnMessagePublished(ITagInfo tagInfo)
    {
    }

    private void OnMessageReceived(ITagInfo tagInfo) => TagDiscoverded?.Invoke(this, new NfcTag
    {
        SerialNumber = tagInfo.SerialNumber,
        Records = tagInfo.Records is null ? [] : tagInfo.Records.Select(r => new NfcRecord
        {
            TypeFormat = (NfcTypeFormat)r.TypeFormat,
            MimeType = r.MimeType,
            Message = r.Message
        })
    });

    public async Task WriteTagAsync(string message, CancellationToken cancellationToken)
    {
        var semaphore = new SemaphoreSlim(0);

        void onTagDiscovered(ITagInfo tagInfo, bool format)
        {
            var record = new NFCNdefRecord
            {
                TypeFormat = NFCNdefTypeFormat.WellKnown,
                MimeType = "text/plain",
                Payload = Encoding.UTF8.GetBytes(message)
            };

            tagInfo.Records = [record];
        }

        void onMessagePublished(ITagInfo tagInfo)
        {
            CrossNFC.Current.StopPublishing();
            CrossNFC.Current.StartListening();

            semaphore.Release();
        }

        CrossNFC.Current.OnTagDiscovered += onTagDiscovered;
        CrossNFC.Current.OnMessageReceived += onMessagePublished;
        CrossNFC.Current.StopListening();

        CrossNFC.Current.StartPublishing();

        await semaphore.WaitAsync(cancellationToken);
    }

    public bool IsAvailable => CrossNFC.Current.IsAvailable;
    public bool IsEnabled => CrossNFC.Current.IsEnabled;
#endif
}
