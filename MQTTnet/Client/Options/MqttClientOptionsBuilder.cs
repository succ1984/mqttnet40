﻿// Decompiled with JetBrains decompiler
// Type: MQTTnet.Client.Options.MqttClientOptionsBuilder
// Assembly: MQTTnet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A57D64C8-A58A-4661-AABB-22ABAFCAAE1A
// Assembly location: C:\Users\ace12\Documents\xinchengbio\code\xc_client\DllMerge\dlls\MQTTnet.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MQTTnet.Client.ExtendedAuthenticationExchange;
using MQTTnet.Formatter;
using MQTTnet.Packets;

namespace MQTTnet.Client.Options
{
  public class MqttClientOptionsBuilder
  {
    private readonly MqttClientOptions _options = new MqttClientOptions();
    private MqttClientTcpOptions _tcpOptions;
    private MqttClientWebSocketOptions _webSocketOptions;
    private MqttClientOptionsBuilderTlsParameters _tlsParameters;
    private MqttClientWebSocketProxyOptions _proxyOptions;

    public MqttClientOptionsBuilder WithProtocolVersion(
      MqttProtocolVersion value)
    {
      _options.ProtocolVersion = value != MqttProtocolVersion.Unknown ? value : throw new ArgumentException("Protocol version is invalid.");
      return this;
    }

    public MqttClientOptionsBuilder WithCommunicationTimeout(TimeSpan value)
    {
      _options.CommunicationTimeout = value;
      return this;
    }

    public MqttClientOptionsBuilder WithCleanSession(bool value = true)
    {
      _options.CleanSession = value;
      return this;
    }

    [Obsolete("This method is no longer supported. The client will send ping requests just before the keep alive interval is going to elapse. As per MQTT RFC the serve has to wait 1.5 times the interval so we don't need this anymore.")]
    public MqttClientOptionsBuilder WithKeepAliveSendInterval(TimeSpan value) => this;

    public MqttClientOptionsBuilder WithNoKeepAlive() => WithKeepAlivePeriod(TimeSpan.Zero);

    public MqttClientOptionsBuilder WithKeepAlivePeriod(TimeSpan value)
    {
      _options.KeepAlivePeriod = value;
      return this;
    }

    public MqttClientOptionsBuilder WithClientId(string value)
    {
      _options.ClientId = value;
      return this;
    }

    public MqttClientOptionsBuilder WithWillMessage(
      MqttApplicationMessage value)
    {
      _options.WillMessage = value;
      return this;
    }

    public MqttClientOptionsBuilder WithAuthentication(
      string method,
      byte[] data)
    {
      _options.AuthenticationMethod = method;
      _options.AuthenticationData = data;
      return this;
    }

    public MqttClientOptionsBuilder WithWillDelayInterval(
      uint? willDelayInterval)
    {
      _options.WillDelayInterval = willDelayInterval;
      return this;
    }

    public MqttClientOptionsBuilder WithTopicAliasMaximum(
      ushort? topicAliasMaximum)
    {
      _options.TopicAliasMaximum = topicAliasMaximum;
      return this;
    }

    public MqttClientOptionsBuilder WithMaximumPacketSize(
      uint? maximumPacketSize)
    {
      _options.MaximumPacketSize = maximumPacketSize;
      return this;
    }

    public MqttClientOptionsBuilder WithReceiveMaximum(
      ushort? receiveMaximum)
    {
      _options.ReceiveMaximum = receiveMaximum;
      return this;
    }

    public MqttClientOptionsBuilder WithRequestProblemInformation(
      bool? requestProblemInformation = true)
    {
      _options.RequestProblemInformation = requestProblemInformation;
      return this;
    }

    public MqttClientOptionsBuilder WithRequestResponseInformation(
      bool? requestResponseInformation = true)
    {
      _options.RequestResponseInformation = requestResponseInformation;
      return this;
    }

    public MqttClientOptionsBuilder WithSessionExpiryInterval(
      uint? sessionExpiryInterval)
    {
      _options.SessionExpiryInterval = sessionExpiryInterval;
      return this;
    }

    public MqttClientOptionsBuilder WithUserProperty(
      string name,
      string value)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (_options.UserProperties == null)
        _options.UserProperties = new List<MqttUserProperty>();
      _options.UserProperties.Add(new MqttUserProperty(name, value));
      return this;
    }

    public MqttClientOptionsBuilder WithCredentials(
      string username,
      string password = null)
    {
      byte[] password1 = null;
      if (password != null)
        password1 = Encoding.UTF8.GetBytes(password);
      return WithCredentials(username, password1);
    }

    public MqttClientOptionsBuilder WithCredentials(
      string username,
      byte[] password = null)
    {
      _options.Credentials = new MqttClientCredentials
      {
        Username = username,
        Password = password
      };
      return this;
    }

    public MqttClientOptionsBuilder WithCredentials(
      IMqttClientCredentials credentials)
    {
      _options.Credentials = credentials;
      return this;
    }

    public MqttClientOptionsBuilder WithExtendedAuthenticationExchangeHandler(
      IMqttExtendedAuthenticationExchangeHandler handler)
    {
      _options.ExtendedAuthenticationExchangeHandler = handler;
      return this;
    }

    public MqttClientOptionsBuilder WithTcpServer(string server, int? port = null)
    {
      _tcpOptions = new MqttClientTcpOptions
      {
        Server = server,
        Port = port
      };
      return this;
    }

    public MqttClientOptionsBuilder WithTcpServer(
      Action<MqttClientTcpOptions> optionsBuilder)
    {
      if (optionsBuilder == null)
        throw new ArgumentNullException(nameof (optionsBuilder));
      _tcpOptions = new MqttClientTcpOptions();
      optionsBuilder(_tcpOptions);
      return this;
    }

    public MqttClientOptionsBuilder WithProxy(
      string address,
      string username = null,
      string password = null,
      string domain = null,
      bool bypassOnLocal = false,
      string[] bypassList = null)
    {
      _proxyOptions = new MqttClientWebSocketProxyOptions
      {
        Address = address,
        Username = username,
        Password = password,
        Domain = domain,
        BypassOnLocal = bypassOnLocal,
        BypassList = bypassList
      };
      return this;
    }

    public MqttClientOptionsBuilder WithProxy(
      Action<MqttClientWebSocketProxyOptions> optionsBuilder)
    {
      if (optionsBuilder == null)
        throw new ArgumentNullException(nameof (optionsBuilder));
      _proxyOptions = new MqttClientWebSocketProxyOptions();
      optionsBuilder(_proxyOptions);
      return this;
    }

    public MqttClientOptionsBuilder WithWebSocketServer(
      string uri,
      MqttClientOptionsBuilderWebSocketParameters parameters = null)
    {
      _webSocketOptions = new MqttClientWebSocketOptions
      {
        Uri = uri,
        RequestHeaders = parameters?.RequestHeaders,
        CookieContainer = parameters?.CookieContainer
      };
      return this;
    }

    public MqttClientOptionsBuilder WithWebSocketServer(
      Action<MqttClientWebSocketOptions> optionsBuilder)
    {
      if (optionsBuilder == null)
        throw new ArgumentNullException(nameof (optionsBuilder));
      _webSocketOptions = new MqttClientWebSocketOptions();
      optionsBuilder(_webSocketOptions);
      return this;
    }

    public MqttClientOptionsBuilder WithTls(
      MqttClientOptionsBuilderTlsParameters parameters)
    {
      _tlsParameters = parameters;
      return this;
    }

    public MqttClientOptionsBuilder WithTls() => WithTls(new MqttClientOptionsBuilderTlsParameters
    {
      UseTls = true
    });

    public MqttClientOptionsBuilder WithTls(
      Action<MqttClientOptionsBuilderTlsParameters> optionsBuilder)
    {
      if (optionsBuilder == null)
        throw new ArgumentNullException(nameof (optionsBuilder));
      _tlsParameters = new MqttClientOptionsBuilderTlsParameters();
      optionsBuilder(_tlsParameters);
      return this;
    }

    public IMqttClientOptions Build()
    {
      if (_tcpOptions == null && _webSocketOptions == null)
        throw new InvalidOperationException("A channel must be set.");
      if (_tlsParameters != null)
      {
        var tlsParameters = _tlsParameters;
        if ((tlsParameters != null ? (tlsParameters.UseTls ? 1 : 0) : 0) != 0)
        {
          var clientTlsOptions1 = new MqttClientTlsOptions
          {
            UseTls = true,
            SslProtocol = _tlsParameters.SslProtocol,
            AllowUntrustedCertificates = _tlsParameters.AllowUntrustedCertificates,
            Certificates = _tlsParameters.Certificates?.ToList(),
#pragma warning disable CS0618 // Type or member is obsolete
            CertificateValidationCallback = _tlsParameters.CertificateValidationCallback,
#pragma warning restore CS0618 // Type or member is obsolete
            CertificateValidationHandler = _tlsParameters.CertificateValidationHandler,
            IgnoreCertificateChainErrors = _tlsParameters.IgnoreCertificateChainErrors,
            IgnoreCertificateRevocationErrors = _tlsParameters.IgnoreCertificateRevocationErrors
          };
          var clientTlsOptions2 = clientTlsOptions1;
          if (_tcpOptions != null)
            _tcpOptions.TlsOptions = clientTlsOptions2;
          else if (_webSocketOptions != null)
            _webSocketOptions.TlsOptions = clientTlsOptions2;
        }
      }
      if (_proxyOptions != null)
      {
        if (_webSocketOptions == null)
          throw new InvalidOperationException("Proxies are only supported for WebSocket connections.");
        _webSocketOptions.ProxyOptions = _proxyOptions;
      }
      _options.ChannelOptions = _tcpOptions ?? (IMqttClientChannelOptions) _webSocketOptions;
      return _options;
    }
  }
}
