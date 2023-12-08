using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IBKR_Trader
{
    public class TwsService
    {
        private EClientSocket _clientSocket;
        private EReaderSignal _signal;
        private int _nextOrderId;

        public TwsService()
        {
            var eWrapperImpl = new EWrapperImpl();
            _clientSocket = eWrapperImpl.ClientSocket;
            _signal = eWrapperImpl.Signal;
            _nextOrderId = 0;
        }

        public void Connect(string host, int port, int clientId)
        {
            _clientSocket.eConnect(host, port, clientId);
            var reader = new EReader(_clientSocket, _signal);
            reader.Start();

            new Thread(() =>
            {
                while (_clientSocket.IsConnected())
                {
                    _signal.waitForSignal();
                    reader.processMsgs();
                }
            })
            { IsBackground = true }.Start();

            while (_nextOrderId <= 0)
            {
                Thread.Sleep(10);
            }
        }

        public EClientSocket ClientSocket
        {
            get { return _clientSocket; }
        }

        private class EWrapperImpl : EWrapper
        {

        }

    }
}
