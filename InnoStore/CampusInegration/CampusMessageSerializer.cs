using System.Text;
using Wolverine;
using Wolverine.Runtime.Serialization;

namespace InnoStore.CampusInegration;

public class CampusMessageSerializer : IMessageSerializer
{
    public string ContentType => "text";

    public object ReadFromData(Type messageType, Envelope envelope)
    {
        var stringData = Encoding.UTF8.GetString(envelope.Data!);
        return stringData;
    }

    public object ReadFromData(byte[] data)
    {
        var stringData = Encoding.UTF8.GetString(data);
        return stringData;
    }

    public byte[] Write(Envelope envelope)
    {
        throw new NotImplementedException();
    }

    public byte[] WriteMessage(object message)
    {
        throw new NotImplementedException();
    }
}
