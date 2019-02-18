namespace Rias.Domain

module Data =
    open System

    type Dto =
        | Number of float
        | String of string
        | Bool of bool
        | List of List<Dto>
        | Map of Map<string, Dto>
        | Null

    [<RequireQualifiedAccess>]
    module Dto =
        open Fable.Core.JsInterop
        open Fable.Core

        let rec toJS dto =
            match dto with
            | Number v -> v :> Object
            | String v -> v :> Object
            | Bool v -> v :> Object
            | Null -> null
            | List list ->
                let arr = ResizeArray<Object>()
                list |> List.iter (fun x -> arr.Add(x))
                arr :> Object
            | Map map ->
                let o = obj()
                map |> Map.iter (fun key v -> o?key <- (toJS v))
                o

        [<Emit("typeof ($0)")>]
        let private jsTypeof (x: obj) : string = jsNative

        [<Emit("Array.isArray($0)")>]
        let private jsIsArray (x: obj) : bool = jsNative

        [<Emit("Object.entries($0)")>]
        let private jsObjectToSeq (obj: obj) : seq<string * obj> = jsNative

        let private typeof (x: obj) : string =
            if isNull x then "null"
            else
                let t = jsTypeof x
                if t = "object" then
                    if jsIsArray x then "array"
                    else t
                else t

        let rec toDto (obj: obj) =
            match typeof obj with
            | "number" -> Number (obj :?> float)
            | "string" -> String (obj :?> string)
            | "boolean" -> Bool (obj :?> bool)
            | "array" ->
                let arr = obj :?> seq<obj>
                let dtoSeq = Seq.map toDto arr 
                List (List.ofSeq dtoSeq)
            | "object" -> 
                let arr = jsObjectToSeq obj
                let dtoSeq = Seq.map (fun (n, v) -> (n, (toDto v))) arr 
                Map (Map.ofSeq dtoSeq)
            | _ -> Null