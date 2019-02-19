namespace Rias.Domain

[<AutoOpen>]
module Aggregate =

    let scmsg (state: StateBox<'state>) (command: CommandBox<'command>) msg =
        sprintf "%s. State: %A, Command: %A" msg state command

    let semsg (state: StateBox<'state>) (event: EventBox<'command>) msg =
         sprintf "%s. State: %A, Event: %A" msg state event

    let extErrorMsg err f x y =
        f x y |> Result.mapError (err x y)

    let create zero execute apply = 
        { Zero = StateBox.zero zero
          Execute = extErrorMsg scmsg execute
          Apply = extErrorMsg semsg (StateBox.apply apply) }
