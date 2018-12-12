namespace Rias.Contract.Persistence

[<AutoOpen>]
module StorageContract =
    open Rias.Contract.Domain

    type Storage<'event> = {
        Store: 'event seq -> Result<unit, string>
        Load: StreamId -> Result<'event seq, string>
    }
