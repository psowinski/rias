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
                load = storage.load >> Result.promiseBind asEvents
                store = Seq.map toDto >> storage.store
            }
        mapped

    let asyncMap storage toDto toEvent = async {
        let! storage = storage
        return map storage toDto toEvent
    }
