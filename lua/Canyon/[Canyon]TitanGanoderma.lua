------------------------------------------------------------------------------------
--Name:		[Canyon]TitanGanoderma
--Purpose:	Defines drops for those monsters.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/07/27
------------------------------------------------------------------------------------

local tTitanGanodermaPotions = {}
tTitanGanodermaPotions["2xExpPotion30Min"] = 728775
tTitanGanodermaPotions["3xExpPotion30Min"] = 728776
tTitanGanodermaPotions["5xExpPotion30Min"] = 728777
tTitanGanodermaPotions["5xExpPotion1Hour"] = 728810

tTitanGanodermaPotions["2xExpPotion1Hour"] = 723017
tTitanGanodermaPotions["3xExpPotion2Hour"] = 720393
tTitanGanodermaPotions["5xExpPotion2Hour"] = 720394


function Canyon_TitanGanodermaDrop()

    local nRate = math.random(1, 100)
    local nUserId = Get_UserId()

    if nRate <= 20 then
        nRate = math.random(1, 100)
        if nRate <= 15 then -- 15% 5x
            Monster_SysDropItem(tTitanGanodermaPotions["5xExpPotion2Hour"], nUserId)
        elseif nRate <= 40 then -- 25% 3x
            Monster_SysDropItem(tTitanGanodermaPotions["3xExpPotion2Hour"], nUserId)
        else
            Monster_SysDropItem(tTitanGanodermaPotions["2xExpPotion1Hour"], nUserId)
        end
    else
        nRate = math.random(1, 100)
        -- rate for exp potions
        if nRate <= 7 then -- 7% 5x exp potion 1 hour
            Monster_SysDropItem(tTitanGanodermaPotions["5xExpPotion1Hour"], nUserId)
        elseif nRate <= 25 then -- 18 % 5x exp potion 30 min
            Monster_SysDropItem(tTitanGanodermaPotions["5xExpPotion30Min"], nUserId)
        elseif nRate <= 50 then -- 25% 3x exp potion 30 min
            Monster_SysDropItem(tTitanGanodermaPotions["3xExpPotion30Min"], nUserId)
        else -- 2x exp potion 30 min
            Monster_SysDropItem(tTitanGanodermaPotions["2xExpPotion30Min"], nUserId)
        end
    end

    nRate = math.random(1, 100)
    if nRate <= 5 then
        Monster_SysDropItem(1088000, nUserId) -- drop dragon ball
        Monster_SysDropItem(1088000, nUserId) -- drop dragon ball
        Monster_SysDropItem(1088000, nUserId) -- drop dragon ball
        Monster_SysDropItem(1088000, nUserId) -- drop dragon ball
        Monster_SysDropItem(1088000, nUserId) -- drop dragon ball
    elseif nRate <= 20 then
        Monster_SysDropItem(1088000, nUserId) -- drop dragon ball
    end

end

------------------------------------------------------------------------------------
-- Monster drops
------------------------------------------------------------------------------------

tMonster[3130] = tMonster[3130] or {}
tMonster[3130]["tFunction"] = tMonster[3130]["tFunction"] or {}
table.insert(tMonster[3130]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3131] = tMonster[3131] or {}
tMonster[3131]["tFunction"] = tMonster[3131]["tFunction"] or {}
table.insert(tMonster[3131]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3132] = tMonster[3132] or {}
tMonster[3132]["tFunction"] = tMonster[3132]["tFunction"] or {}
table.insert(tMonster[3132]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3133] = tMonster[3133] or {}
tMonster[3133]["tFunction"] = tMonster[3133]["tFunction"] or {}
table.insert(tMonster[3133]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3134] = tMonster[3134] or {}
tMonster[3134]["tFunction"] = tMonster[3134]["tFunction"] or {}
table.insert(tMonster[3134]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3135] = tMonster[3135] or {}
tMonster[3135]["tFunction"] = tMonster[3135]["tFunction"] or {}
table.insert(tMonster[3135]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3136] = tMonster[3136] or {}
tMonster[3136]["tFunction"] = tMonster[3136]["tFunction"] or {}
table.insert(tMonster[3136]["tFunction"],Canyon_TitanGanodermaDrop)

tMonster[3137] = tMonster[3137] or {}
tMonster[3137]["tFunction"] = tMonster[3137]["tFunction"] or {}
table.insert(tMonster[3137]["tFunction"],Canyon_TitanGanodermaDrop)

------------------------------------------------------------------------------------