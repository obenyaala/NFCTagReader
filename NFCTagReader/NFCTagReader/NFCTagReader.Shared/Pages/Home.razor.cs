using Microsoft.AspNetCore.Components;
using NFCTagReader.Shared.Models;
using NFCTagReader.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFCTagReader.Shared.Pages;

public partial class Home
{
    [Inject] private INfcReaderWriter NfcReaderWriter { get; set; }

    private NfcTag? _nfcTag;
    private string _messageToWrite = string.Empty;
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender) NfcReaderWriter.TagDiscoverded += OnTagDiscoverded;
        base.OnAfterRender(firstRender);
    }

    private void OnTagDiscoverded(object? sender, NfcTag nfcTag)
    {
        _nfcTag = nfcTag;

        InvokeAsync(StateHasChanged);
    }

    private async Task WriteNfcTagAsync()
    {
        await NfcReaderWriter.WriteTagAsync(_messageToWrite, CancellationToken.None);
    }
}    
