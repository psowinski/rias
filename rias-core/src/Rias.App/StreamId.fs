module Rias.App.StreamId

open Rias.Domain
open System

let generate name =
    StreamId.create name (Guid.NewGuid().ToString())
