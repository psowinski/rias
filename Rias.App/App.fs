namespace Rias

module App =
    open Rias.Common
    open Rias.Contract.Persistence
    open Rias.Contract.Domain

    let getCurrentState aggregate storage streamId = 
        result {
            let! events = storage.Load streamId
            let! state = events |> Seq.fold (bind1of2 aggregate.Apply) (Ok aggregate.Zero)
            return state
        }

    let handleCommand aggregate storage (command: CommandBox<'command>) = 
        result {
            let! state = getCurrentState aggregate storage command.StreamId
            let! events = aggregate.Execute state command
            let! _ = storage.Store (Seq.cast events)
            return events
        }
