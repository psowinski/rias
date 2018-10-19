namespace Rias.AppContract

[<AutoOpen>]
module StorageContract =

    type Storage<'event> = {
        Store: 'event seq -> Result<unit, string>
        Load: string -> Result<'event seq, string>
    }
