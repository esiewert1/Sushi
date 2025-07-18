```mermaid
sequenceDiagram
    participant ConveyorBelt
    participant Diner
    ConveyorBelt->>ConveyorBelt:OnTimerTick
    ConveyorBelt->>ConveyorBelt:MoveOneStep
    ConveyorBelt->>Diner:OnBeltMoved
    par examine belt
        opt not Eating and Sushi<br>in front of me
            Diner->>Diner:Take sushi
            Note right of Diner:Now Eating
            loop N=20
                Diner->>Diner:Chew
            end
            Diner->>Diner:Swallow
            Note right of Diner:Done Eating
        end
    end

```
