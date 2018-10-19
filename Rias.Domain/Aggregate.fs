namespace Rias.Domain

[<AutoOpen>]
module Aggregate =
    open Rias.Common

    let errsc (state: StateBox<'state>) (command: CommandBox<'command>) msg =
        Error (sprintf "%s. State: %A, Command: %A" msg state command)

    let errse (state: StateBox<'state>) (event: EventBox<'command>) msg =
        Error (sprintf "%s. State: %A, Event: %A" msg state event)

    let exterr err f x y =
        f x y |> errbind (err x y)

    let create zero execute apply = 
        { Zero = StateBox.zero zero
          Execute = exterr errsc execute
          Apply = exterr errse (StateBox.apply apply) }
