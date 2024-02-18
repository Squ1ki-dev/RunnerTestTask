using System;
using MessagePipe;

public class CoinsScoreMessageFilter : MessageHandlerFilter<CoinsScoreMessage>
{
    public override void Handle(CoinsScoreMessage message, Action<CoinsScoreMessage> next)
    {
        next.Invoke(new CoinsScoreMessage(message.Score));
    }
}
