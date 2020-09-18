/**
* Copyright 2020 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Watson.TextToSpeech.V1;
using IBM.Watson.TextToSpeech.V1.Model;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Cloud.SDK;


public class TTS_Handler : MonoBehaviour
{
    #region PLEASE SET THESE VARIABLES IN THE INSPECTOR
    [Space(10)]
    [Tooltip("The IAM apikey.")]
    [SerializeField]
    public string iamApikey;
    [Tooltip("The service URL (optional). This defaults to \"https://gateway.watsonplatform.net/text-to-speech/api\"")]
    [SerializeField]
    public string serviceUrl;
    private TextToSpeechService service;

    private string synthesizeText = "Hello, welcome to the Watson Unity SDK!";
    private string synthesizeMimeType = "audio/wav";

    private AudioClip _recording = null;
    private byte[] audioStream = null;
    #endregion

    /*
    */
    private string allisionVoice = "en-US_AllisonVoice";
    private string ES_EnriqueVoice = "es-ES_EnriqueVoice";
    private string ES_LauraVoice = "es-ES_LauraVoice";
    private string es_LA_SofiaVoice = "es-LA_SofiaVoice";
    private string selectedVoice;


    private void Start()
    {
        //LogSystem.InstallDefaultReactors();
        selectedVoice = ES_LauraVoice;
        Runnable.Run(CreateService());
    }

    public void loadText(string text)
    {
        audioStream = null;
        StartListening();
        service.SynthesizeUsingWebsockets(text);
    }

    public bool isAudioReady()
    {
        return service != null && !service.IsListening;
    }

    public AudioClip getAudio()
    {
        if (audioStream != null && audioStream.Length > 0)
        {
            Log.Debug("ExampleTextToSpeech", "Audio stream of {0} bytes received!", audioStream.Length.ToString()); // Use audioStream and play audio
            _recording = WaveFile.ParseWAV("myClip", audioStream);
        }
        return _recording;
    }

    //public IEnumerator getAudio()
    //{
    //    while (service == null || service.IsListening)
    //    {
    //        yield return null;
    //    }

    //    if (audioStream != null && audioStream.Length > 0)
    //    {



    //        Log.Debug("ExampleTextToSpeech", "Audio stream of {0} bytes received!", audioStream.Length.ToString()); // Use audioStream and play audio
    //        _recording = WaveFile.ParseWAV("myClip", audioStream);
    //        PlayClip(_recording);
    //    }
    //    audioStream = null;
            //StartListening();
    //}

    private IEnumerator CreateService()
    {
        if (string.IsNullOrEmpty(iamApikey))
        {
            throw new IBMException("Please add IAM ApiKey to the Iam Apikey field in the inspector.");
        }

        IamAuthenticator authenticator = new IamAuthenticator(apikey: iamApikey);

        while (!authenticator.CanAuthenticate())
        {
            yield return null;
        }

        service = new TextToSpeechService(authenticator);
        if (!string.IsNullOrEmpty(serviceUrl))
        {
            service.SetServiceUrl(serviceUrl);
        }

        Active = true;
    }

    private void OnError(string error)
    {
        Active = false;

        Log.Debug("ExampleTextToSpeech.OnError()", "Error! {0}", error);
    }

    private void StartListening()
    {
        Log.Debug("ExampleTextToSpeech", "start-listening");
        service.Voice = selectedVoice;
        service.OnError = OnError;
        service.StartListening(OnSynthesize);
    }

    public bool Active
    {
        get { return service.IsListening; }
        set
        {
            if (value && !service.IsListening)
            {
                StartListening();
            }
            else if (!value && service.IsListening)
            {
                Log.Debug("ExampleTextToSpeech", "stop-listening");
                service.StopListening();
            }
        }
    }

    private void OnSynthesize(byte[] result)
    {
        Log.Debug("ExampleTextToSpeechV1", "Binary data received!");
        audioStream = ConcatenateByteArrays(audioStream, result);
    }

    #region Synthesize Without Websocket Connection
    //private IEnumerator ExampleSynthesize()
    //{
    //    byte[] synthesizeResponse = null;
    //    AudioClip clip = null;
    //    service.Synthesize(
    //        callback: (DetailedResponse<byte[]> response, IBMError error) =>
    //        {
    //            synthesizeResponse = response.Result;
    //            Log.Debug("ExampleTextToSpeechV1", "Synthesize done!");
    //            clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
    //            PlayClip(clip);
    //        },
    //        text: synthesizeText,
    //        voice: selectedVoice,
    //        accept: synthesizeMimeType
    //    );

    //    while (synthesizeResponse == null)
    //        yield return null;

    //    yield return new WaitForSeconds(clip.length);
    //}
    #endregion
        

    

    #region PlayClip
    
    #endregion

    #region PauseClip
    
    #endregion

    #region Concatenate Byte Arrays
    private byte[] ConcatenateByteArrays(byte[] a, byte[] b)
    {
        if (a == null || a.Length == 0)
        {
            return b;
        }
        else if (b == null || b.Length == 0)
        {
            return a;
        }
        else
        {
            List<byte> list1 = new List<byte>(a);
            List<byte> list2 = new List<byte>(b);
            list1.AddRange(list2);
            byte[] result = list1.ToArray();
            return result;
        }
    }
    #endregion
}

