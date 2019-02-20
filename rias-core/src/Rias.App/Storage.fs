namespace Rias.App

module Storage =
    open Rias.Common
    open Rias.Persistence

    let map storage toDto toEvent =

        let asEvents =
            Seq.map toEvent 
            >> Result.leave
            >> Result.map Seq.cast

        let mapped = 
            {
                load = storage.load >> Result.bind asEvents
                store = Seq.map toDto >> storage.store
            }
        mapped
