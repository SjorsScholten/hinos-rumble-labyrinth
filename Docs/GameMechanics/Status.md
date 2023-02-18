# Status

## Energy Accumulation

When an object is hit by an attack with a selected elemental attribute. A portion of the damage will be accumulated as energy on the object. If the accumulated energy reaches a set threshold, the object will be afflicted by a status condition.

## Attributes

### Resources

| Attribute | Type | Description |
| --- | --- | --- |
| Vitality | Representation | the max health of an entity |

### Movement

| Attribute | Type | Description |
| --- | --- | --- |
| Mobility | Modifier | modifies the movement of an entity |

### Offensive

| Attribute | Type | Description |
| --- | --- | --- |
| Attack | Modifier | modifies the output damage from the entity |
| Accuracy | Modifier | modifies the chance to afflict damage |
| Crit Chance | Modifier | modifies the chance on applying a critical hit |

### Defensive

| Attribute | Type | Description |
| --- | --- | --- |
| Defense | Modifier | modifies the input damage to the entity |
| Evasion | Modifier | modifies the chance to negate damage |
| Vulnerability | Modifier | modifies the rate at which energy is accumulated |
| Resistance | Modifier | modifies the input damage and rate of accumulation of a specific element |
| Crit Chance | Modifier | modifies the chance to receive critical hits |

## Tiers

Some stat attributes are represented by tiers going from F (the lowest) to S (the highest).

| Tier | Modifier | Description |
| --- | --- | --- |
| S | 2.0 | The highest possible tier. |
| A | 1.5 |The target displays an exceptional ability within this stat. |
| B | 1.2 |
| C | 1.0 | The middle tier. |
| D | 0.9 | |
| E | 0.7 | |
| F | 0.4 | The lowest possible tier. |

## Conditions

| Condition | Effect |
| --- | --- |
| Burned | the affected object will take damage based on a percent of max health for the duration of the effect. |
| Hemorrhage | The affected object will have an increased vulnerability for the duration of the effect. |
| Frozen | the affected object will have a reduced mobility for the duration of the effect. |
| Charged | The affected object will have a lowered evasion and increased chance of critical hits for the duration of the effect. |
| Stunned |  The affected object will be unable to move for the duration of the effect. |