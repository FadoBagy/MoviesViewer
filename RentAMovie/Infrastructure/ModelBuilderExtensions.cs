namespace RentAMovie.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using RentAMovie.Data.Models;

    public static class ModelBuilderExtensions
    {
        public static void SeedAdmin(
            this ModelBuilder modelBuilder)
        {
            var adminRole = new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpper()
            };
            modelBuilder
                .Entity<IdentityRole>()
                .HasData(adminRole);

            var adminUser = new User
            {
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "admin@fado.com",
                NormalizedEmail = "admin@fado.com".ToUpper(),
                Photo = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAABBVBMVEX///+wv8b/4IPzsGzP2N3QxOiFkJT19fWvsbO1xMs0PEX/4oS5tbJncXisrrHV3uOelK/5yXhzeoD/5oWdoqb3s207Q0s4P0hCSVEwPEwxQE1GTVU9RUy3i2LArHD7tW3Fx8kwPVBOVV3nqWqAe2F9alrozn3eo2nq6+zh4uPz14BXXWR8gIXNweVMUFQtNkGzqMd+iZC/kWS9tNSjsbjQ0dOlgWCtnWzItHSVmJx6eYyepqs1P0RbZGyJjJHUvXdzb11vYlhWWVehlGmUd12phGGSiqNqa3ugnp3EzdKQnKNhYlmCdF4iNE6TiGXKmGXOqm56dGB1ZlnnvHRhYnGVkamBf5MnXjDMAAAPf0lEQVR4nO2d+1vayBrHRbkORtyIM5NJdglRWzBotEItQmitrRe6256zp+3//6ecmSTkOrkUXAn75Lu/7IOQzId35r3NhG5tFSpUqFChQoUKFSpUqFChQoUKFSpUqFChQoUKFSpUqFChQoVC6tWD6q17QM+tOgCSJQIc1dc9pOdV3yBfji39eWLpIzT66x7Us2pGjg9s/daxdUJm6x7Us0qU/jjYZmr+tmPp8HcirntQz6qCcPP1ryPsj2rBFzIQ1kab41x7pmEYQU+ZTjgzgGFuSBrQUyA5l0AAIJVQBOScQGUjEHsyPD89eNdFA9+LaYQDRN50js6hvAGIPRmdnza3KSJwEWuqziHUVXexWoA7hxuB6ABu+xBHMwzIX++aQcI3XQjwbGS9YwAY4M5GILqALqJqGkQ6/+oAuoQ7h98+nRMCTNUD3AREH6CFiAamIXWP3zcPFi+6hDuHncNvJ4QYpj1FdzYCkXpRD9BChES6Oz3wXvIRWpBHnwiBHqCNmF+P2sMBQIaIvwb4QoRUlBH7AG1EnFNEK0wEcLapTw2+ECGkSPS/nRBiTieqhsKAHEUJI2KI+rpheFINOR0wCyFFlIG6bhyO2tLXg1TATIQ7nU+kvW4cjurgY/N5bLhz+DGXfaoaJB/SETPN0lsIa+k3fHmNQAbELIS3BIzWDcMXQ9xOQcxAmF/ATIjphHkGzIKYSphvwAxrMY0w74AUESeHxWTCzp8E5xyQ5m7SdbwNm81kwsO3RFs3QJr2wHncLG0eHJy+ev+fb6+POoexjOdgb90IKZIXDZko36vrL7LUlQiRbz+/7vABD38n8roRkqWCc74vPXh1TCQAsKZpGABCTuIYz3OZdHvSpWueCZsHd10CzHqfJWO1ft2kjG+5iJ23xFw3RJL6QHrFMWFz+4tkCP6mfV8wyC3Xhq8JyHN3vw0+cEzYPP1IInNPBfD8iGfEW5C/yqlX37NV13l+hlqQwKhd+hDyrMhaxe71ctLMcA8hSBL865TjZL4SxJt4fUQ+cdbiURcSRzk5zjAyoHMIgYoT7ZvvuzHuUQXkWzQyHr49WegjNPKQ4XiHEJg4JvyC4s4kzNBHjhEPO67ycZxBiIvxrglR3HLqIZ4RA2tSeFEWvlIID44T7DAjJzGBf4MImyRhLY0A3HgbNl9JOP6zNUxeJyBuBOHBH1LSIAXy+8YTXktJ7nBG3m484V0KIS/o/8sIN92GzWtpkPDhAfm88YTvu0k7ZXpiyN8MwtMuiK8QeoAcxQNuBuH2QdI+Uh3wEtNNI7yW4tsSZmKwyBNh0xVnmkbr+4XU5Em60/mciyOae+D8/auFOI02Gi/iGoQyPxq+XuhbTrqnGpIWRbnE6SU2t88RP2AM4DnPcufEFcpHB7w2k/FC3fccI9Iif8z53Jhf4n8j7tXkWW62gmu2tgbSHa/Kv+6CaGIzA+QzZ452PpGBe738SeWfVqCIyAw2o/o64gLmvumNeNOUHf+SCBh4lfBoAAKn2QKTFK1x/Omakf9yg2Pz9LhLDDzbU1V1b4YNQk6OuJGw83ds3yofimnrW1tPx6QrSYYBqI+EsRszOW/qb7Gi/Tgmw2kebL+/O/7w5fbvT9924nYQOye5yGKSNIozImNs2k92HcZmaoevc7+Nz+qhOCPalMn7+Cf5PJXo1wB23y1/FuPwDYFJBXMOJKLuu8QTiimnTTpvCMg1Yipg6nkaiojyUE/EaJAKmH4miiHm1ooDkAqY4VxbjifqLANglrOJDDGXec3IIOmAmc6XUsRc7IyG1ZYSA+EvELLMJn+nFVg3g590L0P4dz66FyH1DcI9LbTELH1L8vlzBG3QDSM2w4/M8AjDWWrnM8nhkRpL4zBic/uP0GNPUcLO0e9RQF5bJxcKITa3P3ThXfDRpxBhZ+ctDh7/yjVgCLHZ/CBBJOG70wMP0k/IHs7DBEFye7gpgAFECkiLPVUGpPvBm6weIZ2et4QAWR0BDzH3gD5EG5C+ogoGkLpfToPPAe8cfSSEGIK6ZR1/dxCpF807oIvoAlL12zrsRp/lhnrbiQkMcWMAHUTqZAL9iJTn8S3Eww2YorbGQLp790EKNFzSfnGAIb75tCGAFNEgEgp2lFJ/NWIEEF2WGwJInYuoz4JpV/ovf/Rnuqi+4BifW/+636ehZgxuHKUT1tQXHN0zaGYEN+9TCWumkcu6Pk5toARbu6mEOsR5LSh4Ug3lpwxN30QVpWvnOMNvbnrmI6zpUH5QDPXFR7qk+ga8qVxRK3qI7DjDqXWW4X+cQwgUEN/vU8Rc1r1R1aDyUKmEEGUkdS0tTjUg74CGDbi//13J5yPcEelQqzBdYR9ibYaBX9g7hLAA3N/XlNzvyzCJEF9VbETZb8U4eYD79xhuQJAcg+lFpeIiammINQ3K+wvdK/nPTPfAdL9SyY7IAK8qLuKNko8HgeI1AtP7SiU7og1Y8RAflHxvAveBclMJiLqbBEQKaC/aMxfxu5Lnowo0TnyvhEStKMch1mTbguxtLqKmKLmNGX0TamdhQmui8k8J9zxAP6KsmLm0Yn+sATy9igDSsWv8332igJrv/ReeQ8VAG+cMst/WAUKyrOxzCGMQQ4Ae4o0iyxABt0+1flE8AyEoTMpzKPMIuYgRQNehyorYvjQxREYuIG08JEyeWq1yWZn+4CKeyUoIsUdfCa/ZM8eEuN1uT9oDU1k/ZG+P4QEHr1xuzRHfiAwx8DNzPRwFdBBlZdC2RCF1yCD31vTAM8UDCGMXzxKOMWLlTPMjUkCO17UcKl2FbVcMEmO6Jl8eslc3AYCKjpEPL8mIFNH7scAe5oUVJmpCOGj7NYFYo2sSmS/56HqvLiAAoV4dzqHmB6SS44zoQ4wHvGAmLD/V/YR01lYFC1J4GchaXQAAQf1y2Gg0LqEQJEwwojtR+zFT1DHhvFVulZ/2XEITCtVq9dKCBEL9H853anWR4WkMr1QqNUQ0C9kwwYgWYn+rr8QC2ia0v6qy6kAKilm15ECK/xxkTRUNGtcXeIzQRO0QITUijiO0ENV4C1ITYjR3L+hADhStuhCFhBAYovoPQEbxmDRUKoclw1gjVs6oa8J6HCAzoRb8vijkDMpVnyik8vyQFA9F8UqloQKfwoCtdoIRK1cKVni5q4XPTBieE+XWE8SX1WoIkloSPRtkTR1gtvYGw1IAj2qCcMSEiUY807CCk1ahxrkeRpPhvMqDxIPngFQhs54YxaPLcA718FduGZGTrtjSFflRjtaQCxPKURPS6+lwTu89iUCaMoWE6sqEQJHFCQePEV4iMTqiBCM+KPhx9xFb7VSuCWXO1VoiurRvH4EcmDIGqwLWgdbg4jFCAY05hLFGvIHocXd39xHBm+gf40xYbo2RsPDdpUmIsaqhVTtXOqrGAZYaGprwbNiS4X0UofJzCsa7TGM0jf79Yv9BkblXmyDNG0IYUlz1cH/PgHF8VDJqcIbExsQx4o8paO/amsFIWmCZkPt9lRtIDns4HyI2Vkvl6sCMNWFpCHGp3OKNSosacZ9acGvkIA7gNNQNYCbkOVJ6+RJWhpF7u5D6ig3WwWKV8zQBmHrZyVOEkmPECwXNtmo11UEUoHIR+DvPhPSyTxNRQxhMOHd33I6w4rl3utISZumljmmJivTB8CkEGTbilYIE66FJh3DXVAKRP2rCVutpONARghDrl9wZ5PhTuNKTtDUDJQCWqJedizT0svKtWvIxUiNivxHP2B6Npd4CUfMX+VY64zdhq1Q1GR3UxHmJ78wbzjRVjFWi/gho8ZN0QTmcizLC0AikqEEjaorec5587XuIWrwJSwbESBbnw9hQ5S5FeaVtgD0gpBFakI2hjgPRmhpx6gF+V7C4eLa3tvA2M8VNbqgJleAqfJKxzgrQxPvahCZa5Uz4LMnR+BnnEA1jVyJNZWQ0dhEdbyNjZZHccFbhEFm5WuJNF65mlUMcAkq7jSMML0PF/gQpzjr7CeEFjRXsAVlbj0wi1C4U+JO7CtnnL2koStHcifmrPJOpoWgk4n2bAozEstbCiD+mLLzfKMjd4bZcCFb2nT/ZJoyEVQ2mrRB7IV6u4kxriBNrOYBzCEqRus5xp/tTK0W7oE5fc2T9j8z2OH5OaeTnmZB5U5A2T+14cYnR8s60D3CWSTrEqMqpemSFol1M7TT7fuoLzCwq6tZm+A2cXsRkpK0Zwinfr+NMVzikogI9iys1eWWibcSrhTd5AHuuq7HcqWCXWA+Kcs8zYdkqDBNSxpLrarQVfqKgniVYNC5hTAKuKTcadoKeAkYuYM1OTS0vU6Fh5jtnFTI1EEx25bar0VcooMZATCccKpBXJdruFDttmasp6gVMSOsLy7hn7Bc+YoqK1hwm+4GJExCXP7+RmHcvTGhCkzu+MnOHmmnP0oupGTTh7qN9ruhBMbVI49xFNJPnqe1qhBUejs4SDukklSMNN2eAdTo++3jNz+nMXYZ2arpnJT0XU0C/o2EM4ZOcPE2HKwdEPbGyWCDq4da+h9gqt2gRzvI2UA/ZcBeycKFDscUvMdnHBZji6ZzqYukyvybDLOFwCGPWkW0Hq56POBqaeVPj/phGG64e4ASl3d8J+bGHPVIJYVpAso0oQiV2mFab/4w6Gg/QSUzN6f6Zv4kflQIHKYvECflLH0/pgXCDJEYa5HYVHUQd3uxPBS8aOtWFCO9vuIF08TmauaZ9t3Onflq2VdPPSNiYQBDnLFj6hfB3OI4QDhQNo0iy531sCOAkzc/NV0xqMtS/DqIYzbz9xlAwUn2z1C6CxzT5TjA9DTXpwXjiJDXL1sBqUqMtqEj15NcTxqDvI+w5ARHjBDeToXryCNUlCTMlbZYac5Q04Wjt4ZukC2eKYLyboVM7S2k6XDFty9bDsBFp9h0/4Z4QHUOYcAxR/CcyVIce4fJ9jExp6eJeGHIqqIVFxgj2woSJH6hmmaMe4bKJadYuDVOjCiG3wrBHrKGBz4iqVT0lOKcGhPHbJX6t2KkZJOzKRBETMnBWsAOfNx0xPxNtC3hvT8m4w4RLp96Z+1CWhgp3c8wZs7hoCTvhIilJoGlQpu7JgnD51Nv8JUJWCcd7/zJG7S2PcAB5++OOntIq3wjhsv80jYYmjV+RSYuMWA2hd367/wjhMP6tAp2jGTVfbetCxqbwKzIxFMRYyVifDWyJGpbj3yjA7Pc1mXS87L8R5WtxZhJCSR/w/zX7O9NvyaQsSTjaq/+q9p5Dv37XXD+iUahQoUKFChUqVKhQoUKFChUqVKhQoUKFChUqVKhQoUKFCmXW/wG+0o6Ag9EMCAAAAABJRU5ErkJggg=="
            };
            modelBuilder
                .Entity<User>()
                .HasData(adminUser);
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin123");

            modelBuilder
                .Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                });
        }

        public static void SeedGenres(
            this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 28,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 35,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 16,
                    Name = "Animation"
                },
                new Genre()
                {
                    Id = 9648,
                    Name = "Mystery"
                },
                new Genre()
                {
                    Id = 27,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 18,
                    Name = "Drama"
                },
                new Genre()
                {
                    Id = 53,
                    Name = "Thriller"
                },
                new Genre()
                {
                    Id = 12,
                    Name = "Adventure"
                },
                new Genre()
                {
                    Id = 878,
                    Name = "Science Fiction"
                },
                new Genre()
                {
                    Id = 80,
                    Name = "Crime"
                },
                new Genre()
                {
                    Id = 99,
                    Name = "Documentary"
                },
                new Genre()
                {
                    Id = 14,
                    Name = "Fantasy"
                },
                new Genre()
                {
                    Id = 36,
                    Name = "History"
                },
                new Genre()
                {
                    Id = 10749,
                    Name = "Romance"
                },
                new Genre()
                {
                    Id = 10752,
                    Name = "War"
                },
                new Genre()
                {
                    Id = 37,
                    Name = "Western"
                });
        }
    }
}
