using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Tanks.Model
{

    public class Server : IService
    {
        BasicHttpBinding myBinding = new BasicHttpBinding();
        EndpointAddress myEndpoint = new EndpointAddress("http://mkonjibhu-001-site1.dtempurl.com/Service.svc");
       // EndpointAddress myEndpoint = new EndpointAddress("http://localhost:59057/Service.svc");
        public List<ServerTank> GetList()
        {
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {                
                    client = myChannelFactory.CreateChannel();

                    var list = client.GetList();
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                    return list;
                }
                catch(Exception e)
                {
                    (client as ICommunicationObject)?.Abort();
                    throw e;
                }
            }
        }      
        public void Go_D(long ip)
        {
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {
                    client = myChannelFactory.CreateChannel();
                    client.Go_D(ip );
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                }
                catch (Exception)
                {
                    (client as ICommunicationObject)?.Abort();
                }
            }
        }
          
        public void Go_L(long ip)
        {
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {
                    client = myChannelFactory.CreateChannel();
                    client.Go_L(ip );
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                }
                catch (Exception)
                {
                    (client as ICommunicationObject)?.Abort();
                }
            }
        }

        public void Go_R(long ip)
        {
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {
                    client = myChannelFactory.CreateChannel();
                    client.Go_R(ip );
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                }
                catch (Exception)
                {
                    (client as ICommunicationObject)?.Abort();
                }
            }
        }

        public void Go_U(long ip)
        {
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {
                    client = myChannelFactory.CreateChannel();
                    client.Go_U(ip );
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                }
                catch (Exception)
                {
                    (client as ICommunicationObject)?.Abort();
                }
            }
        }
     
        public void Reg(long ip, float x, float y)
        {
            
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {
                    client = myChannelFactory.CreateChannel();
                    client.Reg(ip,x,y);
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                }
                catch(Exception)
                {
                    (client as ICommunicationObject)?.Abort();
                }
            }
        }

        public void Remuve(long ip)
        {
            using (var myChannelFactory = new ChannelFactory<IService>(myBinding, myEndpoint))
            {
                IService client = null;

                try
                {
                    client = myChannelFactory.CreateChannel();                  
                    client.Remuve(ip);
                    ((ICommunicationObject)client).Close();
                    myChannelFactory.Close();
                   
                }
                catch (Exception e)
                {
                    (client as ICommunicationObject)?.Abort();
                    throw e;
                }
            }
        }

       
    }
}

[ServiceContract]
public interface IService
{
    [OperationContract]
    void Reg(long ip, float x, float y);
    [OperationContract]
    [ServiceKnownType(typeof(ServerTank))]
    List<ServerTank> GetList();
   
    [OperationContract]
    void Go_U(long ip);   
    [OperationContract]
    void Go_D(long ip);    
    [OperationContract]
    void Go_L(long ip);
    [OperationContract]
    void Go_R(long ip);
    [OperationContract]
    void Remuve(long ip);
  



}