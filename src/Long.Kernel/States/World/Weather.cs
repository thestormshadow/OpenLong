using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;

namespace Long.Kernel.States.World
{
    public sealed class Weather
    {
        public const int MAX_WEATHER_INTENSITY = 1000;
        public const int MAX_WEATHER_DIR = 360;
        public const int WEATHER_NORMAL_SPEED = 10;
        public const int WEATHER_FAST_SPEED = 5;

        public const int MAX_KEEP_SECS = 1200;
        public const int MIN_KEEP_SECS = 500;
        public const int WERTHER_FINE_PERCENT = 60;
        public const int WERTHER_CHANGE_DIR_PERCENT = 5;

        public const int WEATHER_RAINY_INTENSITY = MAX_WEATHER_INTENSITY / 5;
        public const int WEATHER_SNOWY_INTENSITY = MAX_WEATHER_INTENSITY / 10;
        public const int WEATHER_DEFAULT_INTENSITY = MAX_WEATHER_INTENSITY / 5;
        public const int WEATHER_DEFAULT_DIR = 10;
        public const int WEATHER_DEFAULT_COLOR = 0x00FFFFFF;
        public const int WEATHER_RAINY_SPEED = WEATHER_NORMAL_SPEED;
        private readonly GameMap owner;

        private readonly TimeOut loop;
        private int currentColor;
        private int currentDirection;
        private int currentIntensity;
        private int currentParticle;

        private WeatherType currentType = WeatherType.WeatherFine;
        private int defaultColor;
        private int defaultDirection;
        private int defaultIntensity;
        private int defaultSpeedSecs;

        private WeatherType defaultType = WeatherType.WeatherFine;

        private int incrementIntensity;
        private int keepSeconds;
        private int speedSeconds;
        private int targetColor;
        private int targetDirection;
        private int targetIntensity;

        private WeatherType targetType = WeatherType.WeatherFine;

        public Weather(GameMap pOwner)
        {
            owner = pOwner;
            loop = new TimeOut(1);
            loop.Startup(1);
        }

        public async Task<bool> CreateAsync(WeatherType type, int intensity, int direction, int color, int speedSeconds)
        {
            if (type <= WeatherType.WeatherNone || type >= WeatherType.WeatherAll)
            {
                return false;
            }

            if (intensity < 0 || intensity >= MAX_WEATHER_INTENSITY)
            {
                return false;
            }

            if (direction < 0 || direction >= MAX_WEATHER_DIR)
            {
                return false;
            }

            if (type == WeatherType.WeatherFine || intensity == 0)
            {
                type = WeatherType.WeatherFine;
                intensity = 0;
            }

            defaultType = type;
            defaultIntensity = intensity;
            defaultDirection = direction;
            defaultColor = color;
            defaultSpeedSecs = speedSeconds;

            currentType = type;
            currentIntensity = intensity;
            currentDirection = direction;
            currentColor = color;
            currentParticle = GetParticle(currentIntensity);

            targetType = type;
            targetIntensity = intensity;
            targetDirection = direction;
            targetColor = color;

            incrementIntensity = 0;
            keepSeconds = await NextAsync(MAX_KEEP_SECS - MIN_KEEP_SECS + 1) + MIN_KEEP_SECS;
            if (type == WeatherType.WeatherCloudy)
            {
                keepSeconds *= 5;
            }

            this.speedSeconds = WEATHER_NORMAL_SPEED;

            return true;
        }

        public async Task<bool> SetNewWeatherAsync(WeatherType type, int intensity, int direction, int color,
                                                   int keepSeconds,
                                                   int speedSeconds)
        {
            if (type <= WeatherType.WeatherNone || type >= WeatherType.WeatherAll)
            {
                return false;
            }

            if (intensity < 0 || intensity >= MAX_WEATHER_INTENSITY)
            {
                return false;
            }

            if (direction < 0 || direction >= MAX_WEATHER_DIR)
            {
                return false;
            }

            if (type == WeatherType.WeatherFine || intensity == 0)
            {
                type = WeatherType.WeatherFine;
                intensity = 0;
            }

            if (keepSeconds == 0)
            {
                defaultType = type;
                defaultIntensity = intensity;
                defaultDirection = direction;
                defaultColor = color;
                defaultSpeedSecs = speedSeconds;
            }

            targetType = type;
            targetIntensity = intensity;
            targetDirection = direction;
            targetColor = color;

            if (currentType == WeatherType.WeatherFine || targetType == WeatherType.WeatherFine
                                                       || currentType == targetType && currentDirection == targetDirection &&
                                                       currentColor == targetColor)
            {
                incrementIntensity = targetIntensity - currentIntensity;
            }
            else
            {
                incrementIntensity = 0 - currentIntensity;
            }

            if (keepSeconds == 0)
            {
                this.keepSeconds = await NextAsync(MAX_KEEP_SECS - MIN_KEEP_SECS + 1) + MIN_KEEP_SECS;
                if (type == WeatherType.WeatherCloudy)
                {
                    this.keepSeconds *= 5;
                }
            }
            else
            {
                this.keepSeconds = keepSeconds;
            }

            this.speedSeconds = speedSeconds;

            return true;
        }

        public new WeatherType GetType()
        {
            return currentType;
        }

        public int GetParticle()
        {
            return currentParticle;
        }

        public int GetParticle(int intensity)
        {
            int particle = intensity + (int)Math.Sqrt(MAX_WEATHER_INTENSITY);
            if (particle >= MAX_WEATHER_INTENSITY)
            {
                particle = MAX_WEATHER_INTENSITY - 1;
            }

            particle = particle * particle / MAX_WEATHER_INTENSITY;

            switch (currentType)
            {
                case WeatherType.WeatherFine:
                    break;
                case WeatherType.WeatherRainy:
                    particle = (particle + 1) / 2;
                    break;
                case WeatherType.WeatherSnowy:
                    particle = (particle + 3) / 4;
                    break;
                case WeatherType.WeatherSands:
                    particle = (particle + 1) / 2;
                    break;
                case WeatherType.WeatherLeaf:
                case WeatherType.WeatherBamboo:
                case WeatherType.WeatherFlower:
                case WeatherType.WeatherFlying:
                case WeatherType.WeatherDandelion:
                    particle = (particle + 24) / 25;
                    break;
                case WeatherType.WeatherWorm:
                    particle = (particle + 29) / 30;
                    break;
                case WeatherType.WeatherCloudy:
                    particle = (particle + 33) / 34;
                    break;
                default:
                    particle = 0;
                    break;
            }

            return particle;
        }

        public int GetDir()
        {
            return currentDirection;
        }

        public int GetColor()
        {
            return currentColor;
        }

        public async Task OnTimerAsync()
        {
            if (!loop.ToNextTime(1))
            {
                return;
            }

            WeatherType oldType = currentType;
            int oldIntensity = currentIntensity;
            int oldParticle = currentParticle;

            if (currentType == targetType && targetType == defaultType &&
                defaultType == WeatherType.WeatherFine)
            {
                return;
            }

            if (currentType == WeatherType.WeatherFine || targetType == WeatherType.WeatherFine
                                                       || currentType == targetType && currentDirection == targetDirection &&
                                                       currentColor == targetColor)
            {
                if (currentType == targetType && (keepSeconds == 0 || --keepSeconds <= 0))
                {
                    WeatherType type = await NextAsync(100) < WERTHER_FINE_PERCENT
                                            ? WeatherType.WeatherFine
                                            : defaultType;

                    int intensity = defaultIntensity - MAX_WEATHER_INTENSITY / 4 +
                                     await NextAsync(MAX_WEATHER_INTENSITY / 2);
                    if (intensity < 1)
                    {
                        intensity = 1;
                    }
                    else if (intensity >= MAX_WEATHER_INTENSITY)
                    {
                        intensity = MAX_WEATHER_INTENSITY - 1;
                    }

                    int direction = type != oldType && await NextAsync(100) < WERTHER_CHANGE_DIR_PERCENT
                                   ? await NextAsync(MAX_WEATHER_DIR)
                                   : targetDirection;
                    int keepSeconds = await NextAsync(MAX_KEEP_SECS - MIN_KEEP_SECS + 1) + MIN_KEEP_SECS;
                    if (type == WeatherType.WeatherCloudy)
                    {
                        keepSeconds *= 5;
                    }

                    await SetNewWeatherAsync(type, intensity, direction, defaultColor, keepSeconds, defaultSpeedSecs);

                    return;
                }

                if (currentType == WeatherType.WeatherFine && targetType != WeatherType.WeatherFine)
                {
                    currentType = targetType;
                    currentIntensity = 0;
                    currentDirection = targetDirection;
                    currentColor = targetColor;

                    incrementIntensity = targetIntensity - 0;
                }

                if (currentType != WeatherType.WeatherFine && incrementIntensity > 0 &&
                    currentIntensity != targetIntensity)
                {
                    currentIntensity = targetIntensity;

                    if (incrementIntensity > 0 && currentIntensity > targetIntensity
                        || incrementIntensity < 0 && currentIntensity < targetIntensity)
                    {
                        currentIntensity = targetIntensity;
                    }
                }
            }
            else // return to fine
            {
                if (currentIntensity != 0)
                {
                    currentIntensity = 0;

                    if (currentIntensity < 0)
                    {
                        currentIntensity = 0;
                    }
                }
            }

            if (currentIntensity != oldIntensity)
            {
                currentParticle = GetParticle(currentIntensity);
            }

            if (currentType != oldType || currentParticle != oldParticle)
            {
                var msg = new MsgWeather
                {
                    ColorArgb = (uint)GetColor(),
                    Direction = (uint)GetDir(),
                    Intensity = (uint)GetParticle(),
                    WeatherType = (uint)GetType()
                };
                await owner.BroadcastMsgAsync(msg);
            }

            if (currentType != WeatherType.WeatherFine && currentIntensity == 0 && incrementIntensity <= 0)
            {
                currentType = WeatherType.WeatherFine;
                currentIntensity = 0;
                currentDirection = 0;
                currentColor = 0;
                currentParticle = 0;

                incrementIntensity = 0;
            }
        }

        public async Task<bool> SendWeatherAsync(Character user = null)
        {
            var msg = new MsgWeather
            {
                ColorArgb = (uint)GetColor(),
                Direction = (uint)GetDir(),
                Intensity = (uint)GetParticle(),
                WeatherType = (uint)GetType()
            };
            if (user != null)
            {
                await user.SendAsync(msg);
            }
            else
            {
                await owner.BroadcastMsgAsync(msg);
            }

            return true;
        }

        public async Task<bool> SendNoWeatherAsync(Character user)
        {
            if (user == null)
            {
                return false;
            }

            var msg = new MsgWeather
            {
                WeatherType = (uint)WeatherType.WeatherFine,
                ColorArgb = 0,
                Direction = 0,
                Intensity = 0
            };
            await user.SendAsync(msg);
            return true;
        }

        public enum WeatherType
        {
            WeatherNone = 0,
            WeatherFine,
            WeatherRainy,
            WeatherSnowy,
            WeatherSands,
            WeatherLeaf,
            WeatherBamboo,
            WeatherFlower,
            WeatherFlying,
            WeatherDandelion,
            WeatherWorm,
            WeatherCloudy,
            WeatherAll
        }
    }
}
