﻿namespace Rias.App

module Processing =
    open Rias.Common
    open Rias.Domain
    open Rias.Persistence
    
    let getCurrentState aggregate storage streamId =
        let applyEventsToState = Seq.fold (Result.bind1of2 aggregate.Apply) (Ok aggregate.Zero)
        let events = storage.load (streamId |> StreamId.toString)
        let state = events |> Result.asyncBind applyEventsToState
        state 

    let handleCommand aggregate storage (command: CommandBox<'command>) = 
            let state = getCurrentState aggregate storage command.StreamId
            let events = (Result.asyncBind1of2 aggregate.Execute) state command
                       |> Result.asyncMap Seq.cast
            let result = events |> AsyncResult.bind storage.store
            result
