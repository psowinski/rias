namespace Rias.Persistence

[<AutoOpen>]
module StorageContract =
    open Rias.Common

    type Storage = {
        Store: Dto seq -> Result<unit, string>
        Load: string -> Result<Dto seq, string>
    }
