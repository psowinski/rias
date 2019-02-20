namespace Rias.App

module Processing =
    open Rias.Common
    open Rias.Domain
    open Rias.Persistence
    
    let getCurrentState aggregate storage streamId = 
        result {
            let! events = storage.load (streamId |> StreamId.toString)
            let! state = events |> Seq.fold (Result.bind1of2 aggregate.Apply) (Ok aggregate.Zero)
            return state
        }

    let handleCommand aggregate storage (command: CommandBox<'command>) = 
        result {
            let! state = getCurrentState aggregate storage command.StreamId
            let! events = aggregate.Execute state command
            let! _ = storage.store (Seq.cast events)
            return events
        }
