namespace StreamingApp.Domain.Static;

public class BadgesData
{
    // TODO: move to DB
    public static List<KeyValuePair<string, string>> GetAllBadges()
    {
        return new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("d97c37bd-a6f5-4c38-8f57-4e4bef88af34", "staff"),

            new KeyValuePair<string, string>("5527c58c-fb7d-422d-b71b-f309dcb85cc1", "broadcaster"),
            new KeyValuePair<string, string>("b817aba4-fad8-49e2-b88a-7cc744dfa6ec", "vip"),
            new KeyValuePair<string, string>("4300a897-03dc-4e83-8c0e-c332fee7057f", "artist-badge"),
            
            new KeyValuePair<string, string>("bbbe0db0-a598-423e-86d0-f9fb98ca1933", "premium"),

            new KeyValuePair<string, string>("aef2cd08-f29b-45a1-8c12-d44d7fd5e6f0", "no_audio"),
            new KeyValuePair<string, string>("199a0dba-58f3-494e-a7fc-1fa0a1001fb8", "no_video"),

            new KeyValuePair<string, string>("ccbbedaa-f4db-4d0b-9c2a-375de7ad947c", "user-anniversary"),
            new KeyValuePair<string, string>("4300a897-03dc-4e83-8c0e-c332fee7057f", "artist-badge"),
            new KeyValuePair<string, string>("d12a2e27-16f6-41d0-ab77-b780518f00a3", "partner"),
            new KeyValuePair<string, string>("3267646d-33f0-4b17-b3df-f923a41db1d0", "moderator"),
            
            new KeyValuePair<string, string>("511b78a9-ab37-472f-9569-457753bbe7d3", "founder"),
            new KeyValuePair<string, string>("5d9f2208-5dd8-11e7-8513-2ff4adfae661", "subscriber"),
            new KeyValuePair<string, string>("25a03e36-2bb2-4625-bd37-d6d9d406238d", "2-Month Subscriber"),
            new KeyValuePair<string, string>("e8984705-d091-4e54-8241-e53b30a84b0e", "3-Month Subscriber"),
            new KeyValuePair<string, string>("2d2485f6-d19b-4daa-8393-9493b019156b", "6-Month Subscriber"),
            new KeyValuePair<string, string>("b4e6b13a-a76f-4c56-87e1-9375a7aaa610", "9-Month Subscriber"),
            new KeyValuePair<string, string>("ed51a614-2c44-4a60-80b6-62908436b43a", "1-Year Subscriber"),

            new KeyValuePair<string, string>("73b5c3fb-24f9-4a82-a852-2f475b59411c", "cheer 1"),
            new KeyValuePair<string, string>("09d93036-e7ce-431c-9a9e-7044297133f2", "cheer 100"),
            new KeyValuePair<string, string>("0d85a29e-79ad-4c63-a285-3acd2c66f2ba", "cheer 1000"),
            new KeyValuePair<string, string>("57cd97fc-3e9e-4c6d-9d41-60147137234e", "cheer 5000"),
            new KeyValuePair<string, string>("68af213b-a771-4124-b6e3-9bb6d98aa732", "cheer 10000"),
            new KeyValuePair<string, string>("64ca5920-c663-4bd8-bfb1-751b4caea2dd", "cheer 25000"),
            new KeyValuePair<string, string>("62310ba7-9916-4235-9eba-40110d67f85d", "cheer 50000"),
            new KeyValuePair<string, string>("ce491fa4-b24f-4f3b-b6ff-44b080202792", "cheer 75000"),
            new KeyValuePair<string, string>("96f0540f-aa63-49e1-a8b3-259ece3bd098", "cheer 100000"),
            new KeyValuePair<string, string>("4a0b90c4-e4ef-407f-84fe-36b14aebdbb6", "cheer 200000"),
            new KeyValuePair<string, string>("ac13372d-2e94-41d1-ae11-ecd677f69bb6", "cheer 300000"),
            new KeyValuePair<string, string>("a8f393af-76e6-4aa2-9dd0-7dcc1c34f036", "cheer 400000"),
            new KeyValuePair<string, string>("f6932b57-6a6e-4062-a770-dfbd9f4302e5", "cheer 500000"),
            new KeyValuePair<string, string>("4d908059-f91c-4aef-9acb-634434f4c32e", "cheer 600000"),
            new KeyValuePair<string, string>("a1d2a824-f216-4b9f-9642-3de8ed370957", "cheer 700000"),
            new KeyValuePair<string, string>("5ec2ee3e-5633-4c2a-8e77-77473fe409e6", "cheer 800000"),
            new KeyValuePair<string, string>("088c58c6-7c38-45ba-8f73-63ef24189b84", "cheer 900000"),
            new KeyValuePair<string, string>("494d1c8e-c3b2-4d88-8528-baff57c9bd3f", "cheer 1000000"),

            new KeyValuePair<string, string>("f1d8486f-eb2e-4553-b44f-4d614617afc1", "1 Gift Subs"),
            new KeyValuePair<string, string>("3e638e02-b765-4070-81bd-a73d1ae34965", "5 Gift Subs"),
            new KeyValuePair<string, string>("bffca343-9d7d-49b4-a1ca-90af2c6a1639", "10 Gift Subs"),
            new KeyValuePair<string, string>("17e09e26-2528-4a04-9c7f-8518348324d1", "25 Gift Subs"),
            new KeyValuePair<string, string>("47308ed4-c979-4f3f-ad20-35a8ab76d85d", "50 Gift Subs"),
            new KeyValuePair<string, string>("5056c366-7299-4b3c-a15a-a18573650bfb", "100 Gift Subs"),
            new KeyValuePair<string, string>("df25dded-df81-408e-a2d3-40d48f0d529f", "250 Gift Subs"),
            new KeyValuePair<string, string>("f440decb-7468-4bf9-8666-98ba74f6eab5", "500 Gift Subs"),
            new KeyValuePair<string, string>("b8c76744-c7e9-44be-90d0-08840a8f6e39", "1000 Gift Subs"),
        };
    }
}
