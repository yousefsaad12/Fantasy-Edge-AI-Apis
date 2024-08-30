

namespace Api.Mapping
{
    public static class PlayerTransferMapping
    {
        public static PlayerTransfer MapPlayerTransfer(this PlayerJsonForm playerJson)
        {
            return new PlayerTransfer
            {
                TransfersIn = playerJson.transfers_in,
                TransfersInEvent = playerJson.transfers_in_event,
                TransfersOut = playerJson.transfers_out,
                TransfersOutEvent = playerJson.transfers_out_event,
                
                PlayerId = playerJson.id // Assuming PlayerId is mapped to id
            };
        }

    }
}