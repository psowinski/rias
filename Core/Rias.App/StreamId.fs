namespace Rias.App

module StreamId =
    open System
    open Rias.Contract.Domain

    let generate name =
        StreamId.create name (Guid.NewGuid().ToString())
