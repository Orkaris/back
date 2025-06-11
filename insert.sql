DELETE FROM t_e_sport_spo;
DELETE FROM t_e_user_usr;
DELETE FROM "t_e_Exercise_exr";
DELETE FROM t_e_exercise_goal_exg;
DELETE FROM t_e_category_cat;
DELETE FROM "t_e_Workout_wkt";
DELETE FROM "t_email_confirmation_tokens_ect";
DELETE FROM t_j_exercise_category_ext;
DELETE FROM t_e_session_ses;
DELETE FROM t_e_exercise_goal_performance_egp;
DELETE FROM t_e_session_performance_spe;
DELETE FROM t_j_session_exercise_goal_seg;
DELETE FROM t_e_exercise_muscle_link;
DELETE FROM t_e_muscle_mus;

INSERT INTO t_e_sport_spo (spo_id, spo_name) VALUES
('45efb5a5-8d94-49de-bb47-fa31bf246fb2', 'Fitness'),
('c2ef8d6e-5b7a-423d-a8f7-26ff4c84cd90', 'Swimming'),
('c4e4e694-5b9f-40d3-87ac-75116d428a1f', 'Bodybuilding'),
('bfb037e9-8b4e-4a71-a502-ca9b3e5729fa', 'Powerlifting'),
('880669b5-8d88-47fe-ab7c-f74f16726eeb', 'Dance'),
('154f3377-314d-4624-a30d-3f1fa9b86e5d', 'Boxing'),
('a89cbc13-619a-462b-94e8-a6c2c3540f73', 'Cycling'),
('49ccb905-6c72-4d3b-84bf-a1f1df53c87e', 'Rowing'),
('68a556ad-3899-4851-9859-ebc62bb1bea6', 'Running'),
('18200005-c734-4f40-b7e8-9f861862560a', 'Climbing'),
('43d8a05c-9fb8-4166-af2d-e282b133bff6', 'Tennis'),
('a1dd0960-8e05-4e71-872e-e7e47e6107ec', 'Gymnastics'),
('686f84c1-8346-4a65-8811-666f589b1dab', 'Weightlifting'),
('5a17dd0f-2850-48c4-9f80-72230c491f46', 'Pilates'),
('4bc38686-db47-4c7c-a2c9-c3f65598bd91', 'Crossfit'),
('bd018785-ab26-4561-8b81-f3efeabddba7', 'Football'),
('292a568f-f866-4d8d-b669-02aae7cc8a1e', 'Yoga'),
('b9777752-66ef-43d1-881d-f6f6cee8f588', 'Basketball'),
('906bfbca-d185-4e18-9b3a-370a017fcbd1', 'HIIT'),
('1f596089-83b2-4e6a-9a9a-a4f694c22b09', 'Martial Arts');

INSERT INTO t_e_user_usr (usr_id, usr_name, usr_email, usr_password, usr_gender, usr_height, usr_weight, usr_birth_date, usr_profile_type, usr_is_verified, usr_created_at) VALUES
('d3d185fc-b187-480b-b81c-2da474186b14', 'Allison Hill', 'jillrhodes@miller.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 152, 108, '1982-05-23', 3, 'True', '2025-02-08T17:32:31'),
('4a51ac07-9a45-446a-a6e6-9b783b8d888b', 'Joe Martinez', 'arnoldmaria@hotmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 155, 120, '1976-01-14', 2, 'True', '2024-07-11T16:40:10'),
('0574cdf2-034a-4868-91b9-bed150ee077d', 'Carla Gray', 'megan03@trujillo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 162, 58, '1970-07-30', 1, 'True', '2024-12-30T20:22:52'),
('c92821e0-0c62-492e-9885-5c7e120c4a7c', 'Brian Burton', 'cruzcaitlin@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 155, 79, '1984-09-12', 1, 'True', '2025-03-07T13:26:03'),
('bfe11975-4dd1-4105-95c6-d7f951178f40', 'Lisa Hernandez', 'georgetracy@hickman.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 179, 96, '1991-07-18', 1, 'True', '2024-10-26T12:43:58'),
('a6c9ef51-3d63-4637-9795-80422b604adc', 'Jessica Holmes', 'jeffreymorgan@spencer.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 163, 84, '1968-01-29', 3, 'True', '2024-10-27T20:00:19'),
('5385d627-10f0-4ac7-8fe4-133dac83f277', 'Maria Brown', 'wrightjames@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 190, 71, '1975-08-06', 3, 'True', '2024-03-13T04:09:05'),
('4cbbe024-7603-4395-9215-338628eabbd6', 'James House', 'rodney87@gmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 179, 98, '2003-02-27', 2, 'True', '2023-12-01T01:22:02'),
('31453ead-c7d2-427f-b523-03e8056b8172', 'Amanda Miller', 'james53@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 170, 57, '1994-03-18', 1, 'True', '2025-03-01T09:42:46'),
('1140d284-a9e8-44b1-8615-7392bdbc40ae', 'Elizabeth Riggs', 'sarah12@wilson-rodriguez.net', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 175, 84, '1994-06-16', 1, 'True', '2024-07-25T07:38:56'),
('54ce14f4-b8ef-4e0c-a24c-71906d98cf34', 'Kristina Rodriguez', 'davenportbrandi@jordan.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 195, 90, '1986-12-30', 1, 'True', '2025-01-20T12:19:18'),
('983f6638-cc72-4ecc-85af-9f438078f84d', 'Rebekah Shields', 'erik16@garrison.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 191, 108, '2003-11-25', 1, 'True', '2023-12-16T00:13:56'),
('65eb8310-b56b-4a63-a9fa-43ee9c651eaa', 'Kyle Reed', 'gobrien@gmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 165, 118, '1998-11-25', 2, 'True', '2024-01-19T01:03:52'),
('637b0bb9-b869-4232-ba30-be17ac59a6fb', 'Erika Terry', 'robertbentley@adkins.biz', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 175, 96, '1980-05-16', 1, 'True', '2024-10-27T09:24:03'),
('94f3c169-1dc9-4c62-ba59-6df41d4cc291', 'Robert Hernandez', 'traceycarr@gibson.org', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 181, 61, '2000-07-14', 1, 'True', '2024-07-30T22:42:11'),
('8ea21fe2-7e92-44ca-90ff-38f3e8b90cdf', 'Tiffany Vance', 'vmerritt@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 190, 70, '1968-10-05', 3, 'True', '2024-04-10T01:16:43'),
('92471d85-6ffd-418d-a6fa-996a7ad7a441', 'Joshua Perry', 'xprice@shah.org', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 154, 99, '1996-03-13', 2, 'True', '2025-05-07T01:04:43'),
('e4c093f7-daa9-4d03-96d2-efaa731d3b5b', 'Marcus Johnson', 'millerroy@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 166, 120, '1966-07-03', 1, 'True', '2023-12-01T21:16:31'),
('2473b665-d3be-48b2-80e6-9a2085e4befd', 'Alex Hernandez', 'kcaldwell@marshall.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 184, 84, '1984-11-29', 3, 'True', '2025-03-14T06:11:43'),
('b2e772e1-983f-4c66-b7d4-76295626503f', 'Renee Hogan', 'amanda51@holland.info', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 168, 105, '1995-01-25', 1, 'True', '2023-11-29T05:31:28'),
('708788c2-4c5e-4e14-9139-dfa951565db8', 'Laura Barnett', 'snydermark@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 196, 83, '1979-06-21', 3, 'True', '2024-04-01T22:09:50'),
('867c080a-94a9-4d17-990f-97a6cce892f0', 'Elizabeth Perkins', 'ronald85@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 156, 88, '1976-08-08', 3, 'True', '2024-01-13T14:15:24'),
('aa2cdd55-ecd0-4ec4-8505-887bb522c1de', 'Joseph Hill', 'heidi27@salas.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 173, 70, '2005-08-26', 3, 'True', '2024-08-28T20:11:25'),
('1f9ff58a-2131-46c8-84fc-4b5cd8422239', 'Courtney Mills', 'levans@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 170, 112, '1983-08-19', 1, 'True', '2024-11-19T20:13:06'),
('0b97526b-2d44-46eb-a163-3d167b93637e', 'Andrea Hall', 'ronaldstephens@hotmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 169, 80, '1986-04-29', 1, 'True', '2024-09-03T17:37:06'),
('c31237f9-96d7-4926-b416-e4cab853e890', 'Brandon Hayden', 'barbara27@pearson.org', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 155, 60, '1964-06-20', 3, 'True', '2024-10-15T15:15:31'),
('79549b5c-5001-49cb-a3b6-5fc711adcee4', 'Julia Brown', 'wgarrett@gmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 198, 118, '1998-04-22', 1, 'True', '2025-05-17T05:29:37'),
('80a25ef8-dc8e-4c12-b292-6590d4cac7b8', 'Katrina Hill', 'jsanchez@gmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 180, 120, '1997-05-08', 1, 'True', '2024-02-15T21:09:25'),
('404ba29b-49e4-4af5-8d41-0e40b9a48308', 'Phillip Bennett', 'tinaallen@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 188, 104, '1974-03-15', 1, 'True', '2024-04-06T06:08:09'),
('e880a8b3-9c26-4ce5-a3a7-882a52013f5b', 'Tracy Jones', 'cindy61@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 169, 101, '1984-10-08', 3, 'True', '2025-04-24T12:37:32'),
('fa1238b1-b378-4489-aff5-c82d320219f8', 'Frederick Brown', 'ryan38@yahoo.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Female', 183, 107, '1994-06-14', 1, 'True', '2024-02-16T05:23:26'),
('6c28ff8d-d57a-483b-b065-4b0438492c02', 'Elizabeth Hayden DDS', 'james67@gmail.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Male', 154, 93, '1979-04-18', 1, 'True', '2024-09-29T06:59:52'),
('a17a3530-6505-43d9-ae45-5e21f2d4deae', 'David Campos', 'bergerdebbie@kramer-johnson.com', '$2a$11$BwRicObYa1hCZ1GCQG108eEjuheCXF9JZeDzPlWKps1M0gtVZnoGG', 'Other', 164, 50, '1996-04-05', 1, 'True', '2023-07-21T07:15:44');

INSERT INTO "t_e_Exercise_exr" (exr_id, exr_name, exr_description, exr_created_at) VALUES
('9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', 'Bench Press', 'A compound exercise targeting the chest, shoulders, and triceps.', '2023-08-14T10:32:15'),
('6b2f3c23-6e12-45cc-b60a-0a9e46ae1542', 'Incline Bench Press', 'Focuses on the upper chest and front deltoids.', '2023-05-22T14:28:19'),
('7dcaee8c-e63f-4c28-b893-f3cd91f905a2', 'Decline Bench Press', 'Emphasizes the lower part of the chest.', '2024-01-10T09:17:42'),
('2c42c2ea-d4f3-45b2-a9b5-3128493e47e1', 'Dumbbell Fly', 'Isolates the chest muscles through a wide range of motion.', '2022-11-05T13:21:03'),
('8f561689-5db6-42d2-baf0-f2751e7c3c93', 'Push-Up', 'Bodyweight exercise primarily targeting the chest and triceps.', '2024-03-02T18:10:34'),
('abf67e29-b2cc-42f4-9057-09d820fa55dc', 'Cable Crossover', 'Isolates the chest using cables for constant tension.', '2024-02-11T08:44:12'),
('f9270ae7-8374-4602-8650-21643f28e405', 'Chest Dip', 'Targets the lower chest and triceps.', '2023-06-23T15:02:08'),
('b1f78aa1-8f07-40db-bd07-e0b2689ec3b2', 'Pull-Up', 'Compound back exercise emphasizing lats and biceps.', '2024-05-01T12:46:21'),
('8393a9c1-d25f-4748-845c-3f761bf6a7a7', 'Chin-Up', 'Similar to pull-ups but focuses more on the biceps.', '2022-12-13T11:24:40'),
('d7099284-bc95-4199-a2f6-0e25e7d71245', 'Lat Pulldown', 'Mimics pull-ups for those unable to perform bodyweight versions.', '2023-07-07T17:11:06'),
('b310f63c-b2f6-4db5-84b3-51c8f9e3cb29', 'Barbell Row', 'Back-focused row movement for strength and thickness.', '2023-10-15T09:40:00'),
('64dbefdc-81a0-47e4-9c62-8b1c870fe85a', 'Seated Cable Row', 'Controlled movement for lats and rhomboids.', '2024-01-12T14:19:17'),
('09db924c-07f7-423f-beb6-71e2de01a685', 'Dumbbell Row', 'Unilateral back exercise improving symmetry and strength.', '2023-09-04T08:22:41'),
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', 'Deadlift', 'Full-body compound lift emphasizing the posterior chain.', '2024-02-28T07:33:56'),
('be4c1451-8164-4a6f-9971-937832ee2f88', 'Sumo Deadlift', 'Deadlift variation targeting quads and inner thighs.', '2024-01-09T10:58:32'),
('5e3fae43-0285-405d-93d6-0b4f0f1c6f2a', 'Romanian Deadlift', 'Hamstring and glute focused posterior chain exercise.', '2023-06-11T11:49:13'),
('f89ea42b-56d2-4c36-8f4e-f76f8498f9df', 'Squat', 'Classic lower-body movement working quads, glutes, and hamstrings.', '2024-03-15T09:29:10'),
('438a9b66-6f5b-4b57-a21c-7d9a361e3a65', 'Front Squat', 'More emphasis on quads than traditional back squats.', '2023-08-20T13:58:21'),
('147c4e57-3e30-4433-9c87-33a2410be1e3', 'Leg Press', 'Machine-based quad dominant pressing movement.', '2023-11-05T15:40:45'),
('f305683c-15ef-446d-bf71-9aa8c388f520', 'Leg Extension', 'Isolation movement for quadriceps.', '2022-10-22T14:12:50'),
('ce5e3a14-91cf-4ddf-bd1f-e31d8b49a548', 'Leg Curl', 'Targets the hamstrings in isolation.', '2024-04-01T10:03:14'),
('3f5933e5-b1e0-4b43-8495-ea0eabafda3e', 'Calf Raise', 'Focused on developing the calf muscles.', '2023-12-07T18:00:22'),
('ea8260a1-4f31-4c7f-b2a3-06fd9e2c8be3', 'Standing Calf Raise', 'Calf exercise emphasizing gastrocnemius.', '2023-11-15T12:20:31'),
('e191a1b2-4866-4dfb-a49f-1e2d07c3f1dc', 'Seated Calf Raise', 'Targets the soleus muscle in the calves.', '2024-02-07T11:10:05'),
('1a2b3c4d-5e6f-7081-9201-aefaccddeeff', 'Barbell Curl', 'Bicep curling exercise with a barbell.', '2023-10-11T13:14:15'),
('22334455-6677-8899-aabb-ccddeeff0011', 'Dumbbell Curl', 'Unilateral bicep curl for isolation and balance.', '2024-01-20T09:47:28'),
('aabbccdd-eeff-0011-2233-445566778899', 'Preacher Curl', 'Isolated biceps movement for peak contraction.', '2024-03-22T08:00:00'),
('33445566-7788-99aa-bbcc-ddeeff001122', 'Concentration Curl', 'Strict biceps curl focusing on muscle isolation.', '2023-07-18T15:30:00'),
('11223344-5566-7788-99aa-bbccddeeff00', 'Overhead Triceps Extension', 'Triceps stretch and strengthen in the overhead position.', '2024-04-15T16:00:00'),
('8899aabb-ccdd-eeff-0011-223344556677', 'Skull Crusher', 'Lying triceps extension for mass and strength.', '2023-09-23T11:30:00'),
('778899aa-bbcc-ddee-ff00-112233445566', 'Triceps Kickback', 'Isolation movement for the triceps.', '2023-12-18T13:20:00'),
('66778899-aabb-ccdd-eeff-001122334455', 'Cable Triceps Pushdown', 'Cable-based triceps extension with tension.', '2024-03-29T09:15:00'),
('55667788-99aa-bbcc-ddee-ff0011223344', 'Military Press', 'Overhead press movement for shoulders.', '2023-08-08T14:50:00'),
('44556677-8899-aabb-ccdd-eeff00112233', 'Dumbbell Shoulder Press', 'Targets front and medial deltoids.', '2023-10-10T12:10:00'),
('ee334455-6677-8899-aabb-ccddeeff0011', 'Front Raise', 'Front deltoid-focused shoulder movement.', '2024-01-11T08:35:00'),
('9845fefe-5566-7788-99aa-bbccddeeff00', 'Rear Delt Fly', 'Targets the rear delts and upper back.', '2024-04-12T15:10:00'),
('aabbc988-eeff-0011-2233-445566778899', 'Shrug', 'Traps isolation movement.', '2023-06-30T18:00:00'),
('99887766-5544-3322-1100-aabbccddeeff', 'Plank', 'Core isometric hold for stability.', '2024-05-01T07:15:00'),
('1100aabb-ccdd-eeff-9988-776655443322', 'Crunch', 'Abdominal flexion movement.', '2024-05-10T10:10:00'),
('eeff0011-2233-4455-6677-8899aabbccdd', 'Leg Raise', 'Targets lower abdominal muscles.', '2023-07-25T13:30:00'),
('1af8d899-aabb-ccdd-eeff-00112e334455', 'Russian Twist', 'Rotational core exercise engaging obliques.', '2023-12-14T11:45:00'),
('5562ae88-9eaa-bbcc-ddee-ff0011223344', 'Mountain Climber', 'Dynamic core and cardio movement.', '2024-03-03T16:20:00'),
('a556677e-8899-aabb-ccdd-eeea0a112233', 'Cable Woodchopper', 'Rotational core exercise hitting the obliques.', '2024-04-29T16:38:20');

INSERT INTO t_e_exercise_goal_exg (exg_id, exg_reps, exg_sets, exr_created_at, exr_id, exg_weight) VALUES
('a1c2345d-6789-4bcd-a123-567890abcdef', 10, 4, '2024-05-01T08:00:00Z', '9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', 60.0), -- Bench Press
('b2d3456e-7890-5cde-b234-678901bcdef0', 12, 3, '2024-05-01T08:05:00Z', '778899aa-bbcc-ddee-ff00-112233445566', 10.0), -- Triceps Kickback
('c3e4567f-8901-6def-c345-789012cdef01', 5, 5, '2024-05-01T08:10:00Z', '6a345e31-2ba9-41ef-a23e-15cd9819fa89', 120.0), -- Deadlift
('d4f56780-9012-7ef0-d456-890123def012', 8, 4, '2024-05-01T08:15:00Z', 'f89ea42b-56d2-4c36-8f4e-f76f8498f9df', 100.0), -- Squat
('e5a67891-0123-8f01-e567-901234ef0123', 10, 4, '2024-05-01T08:20:00Z', '55667788-99aa-bbcc-ddee-ff0011223344', 40.0), -- Military Press
('f6b78902-1234-9f12-f678-012345f01234', 15, 3, '2024-05-01T08:25:00Z', 'eeff0011-2233-4455-6677-8899aabbccdd', 0.0); -- Leg Raise

INSERT INTO t_e_category_cat (cat_id, cat_name, spo_id, cat_created_at) VALUES
('c7901cf3-d07a-4a3d-94a7-fd38c5722c91', 'Chest', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:00:00Z'),
('2cf87976-8d6b-4b35-a1ff-4c1135db7bd1', 'Back', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:01:00Z'),
('1358e12f-3546-4376-bdc7-b4971779aa6a', 'Shoulders', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:02:00Z'),
('fce19a66-9bfa-4a17-85f3-e3738efb3482', 'Biceps', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:03:00Z'),
('6d4f9e70-87df-46a6-83ef-5a0df8c84226', 'Triceps', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:04:00Z'),
('91f47dc7-1b7e-4aa9-b09e-d87c7ef1abf6', 'Forearms', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:05:00Z'),
('4ff2e206-e6cf-442f-8a56-dad168d14e62', 'Quadriceps', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:06:00Z'),
('a2a6ce69-302f-41ae-9b83-8b3b30c1735c', 'Hamstrings', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:07:00Z'),
('04e8ff9e-3903-44cd-8411-5410c9fda663', 'Glutes', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:08:00Z'),
('6e59d9f2-2321-4a7f-818e-40cf17f30bde', 'Calves', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:09:00Z'),
('879f0e2c-bf20-4c91-90ee-f58b730fd218', 'Abdominals', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:10:00Z'),
('11938939-dfd7-4d47-8915-b4e7e0024ac4', 'Obliques', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:11:00Z'),
('3f46e6df-b0f6-4f65-b2f3-860bc3cc546f', 'Trapezius', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:12:00Z'),
('c6e80bc7-9b56-4e0b-85f3-41e43b5d384f', 'Neck', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:13:00Z'),
('4090e1fb-3734-4a2e-987b-e1534bfc5e41', 'Lower Back', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:14:00Z'),
('c470a34b-d1a5-4a1b-a4ff-6844bb60d6d9', 'Upper Chest', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:15:00Z'),
('fa949b0c-2e2c-4d18-9323-0a2737b42662', 'Front Deltoid', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:18:00Z'),
('2fbe9e7a-b210-4433-9153-f2cf63c45d6d', 'Lateral Deltoid', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:19:00Z'),
('e4b9fc26-61d6-4e67-97dc-490a2f6fa4c7', 'Rear Deltoid', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:20:00Z'),
('fa28506e-7e9f-4f27-8901-b39c5c426191', 'Thighs', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:21:00Z'),
('2048280e-5984-47f1-9c5e-3bc6a9fcb758', 'Hip Flexors', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:23:00Z'),
('08c97c82-c91b-4fd5-b94d-51e4ea15ee47', 'Adductors', 'c4e4e694-5b9f-40d3-87ac-75116d428a1f', '2024-05-01T09:24:00Z');


INSERT INTO "t_e_Workout_wkt" (pfr_id, pfr_name, usr_id, pfr_created_at) VALUES
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'Full Body Routine', 'd3d185fc-b187-480b-b81c-2da474186b14', '2024-05-01T10:00:00Z'),
('7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'Push Day', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', '2024-05-02T10:00:00Z'),
('f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'Leg Day', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', '2024-05-01T11:00:00Z'),
('2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'Pull Day', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', '2024-05-03T11:00:00Z'),
('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'Upper Body Hypertrophy', '0574cdf2-034a-4868-91b9-bed150ee077d', '2024-05-02T12:00:00Z'),
('ae5cb69b-2789-4c94-8ef8-9267ef034268', 'Glutes and Core', '0574cdf2-034a-4868-91b9-bed150ee077d', '2024-05-04T12:00:00Z');

INSERT INTO t_email_confirmation_tokens_ect (ect_id, ect_token, usr_id, ect_expiration_date, ect_is_used) VALUES
('b3ef3143-b2e3-445c-93c3-fd95e11de34a', 'cf7e76ba7581428f9cc29963d29a1f96', 'd3d185fc-b187-480b-b81c-2da474186b14', '2024-06-01T10:00:00Z', false),
('fe3ed71f-3a94-456e-b31a-cf0ea2d8260e', '6b9e44df0ec74c9caa927b8ac0bb9dd2', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', '2024-06-01T10:00:00Z', true),
('29ff8dc4-846e-497e-b4c4-67e61c3fc6df', 'd165a1b948644ec2a3cf56977e4e30ef', '0574cdf2-034a-4868-91b9-bed150ee077d', '2024-06-01T10:00:00Z', false);

INSERT INTO t_j_exercise_category_ext (exr_id, cat_id) VALUES
-- Bench Press: Chest, Triceps
('9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', 'c7901cf3-d07a-4a3d-94a7-fd38c5722c91'),
('9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', '6d4f9e70-87df-46a6-83ef-5a0df8c84226'),

-- Triceps Kickback: Triceps
('778899aa-bbcc-ddee-ff00-112233445566', '6d4f9e70-87df-46a6-83ef-5a0df8c84226'),

-- Deadlift: Back, Biceps, Quadriceps
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', '2cf87976-8d6b-4b35-a1ff-4c1135db7bd1'),
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', 'fce19a66-9bfa-4a17-85f3-e3738efb3482'),
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', '4ff2e206-e6cf-442f-8a56-dad168d14e62'),

-- Squat: Quadriceps
('f89ea42b-56d2-4c36-8f4e-f76f8498f9df', '4ff2e206-e6cf-442f-8a56-dad168d14e62'),

-- Military Press: Shoulders, Triceps
('55667788-99aa-bbcc-ddee-ff0011223344', 'c7901cf3-d07a-4a3d-94a7-fd38c5722c91'),
('55667788-99aa-bbcc-ddee-ff0011223344', '6d4f9e70-87df-46a6-83ef-5a0df8c84226'),

-- Leg Raise: Abdominals
('eeff0011-2233-4455-6677-8899aabbccdd', '879f0e2c-bf20-4c91-90ee-f58b730fd218');


INSERT INTO t_e_session_ses (ses_id, ses_name, usr_id, wrk_id, ses_created_at, ses_duration) VALUES
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'Full Body Routine', 'd3d185fc-b187-480b-b81c-2da474186b14', '1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', '2024-05-01T08:00:00Z', '01:12:34'),
('7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'Push Day', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', '7351fd9b-6f6e-4bb5-b66a-73e180408d65', '2024-05-02T08:00:00Z', '01:12:34'),
('f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'Leg Day', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', 'f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', '2024-05-03T08:00:00Z', '01:12:34'),
('2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'Pull Day', '4a51ac07-9a45-446a-a6e6-9b783b8d888b', '2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', '2024-05-04T08:00:00Z', '01:12:34'),
('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'Upper Body Hypertrophy', '0574cdf2-034a-4868-91b9-bed150ee077d', '4cd4b80a-25db-419d-b37a-45c3e3371a2c', '2024-05-05T08:00:00Z', '01:12:34'),
('ae5cb69b-2789-4c94-8ef8-9267ef034268', 'Glutes and Core', '0574cdf2-034a-4868-91b9-bed150ee077d', 'ae5cb69b-2789-4c94-8ef8-9267ef034268', '2024-05-06T08:00:00Z', '01:12:34');


INSERT INTO t_e_exercise_goal_performance_egp (
    egp_id, egp_reps, egp_sets, egp_created_at, exg_id, egp_weight
) VALUES
-- Bench Press (goal: 10 reps, 4 sets)
('11aa22bb-3344-55cc-6677-8899aabbccdd', 10, 4, '2024-05-02T09:00:00Z', 'a1c2345d-6789-4bcd-a123-567890abcdef', 80),

-- Triceps Kickback (goal: 12 reps, 3 sets)
('22bb33cc-4455-66dd-7788-99aabbccdde1', 11, 3, '2024-05-02T09:05:00Z', 'b2d3456e-7890-5cde-b234-678901bcdef0', 25),

-- Deadlift (goal: 5 reps, 5 sets)
('33cc44dd-5566-77ee-8899-aabbccddeeff', 5, 4, '2024-05-02T09:10:00Z', 'c3e4567f-8901-6def-c345-789012cdef01', 120),

-- Squat (goal: 8 reps, 4 sets)
('44dd55ee-6677-88ff-9900-bbccddeeff00', 8, 3, '2024-05-02T09:15:00Z', 'd4f56780-9012-7ef0-d456-890123def012', 100),

-- Military Press (goal: 10 reps, 4 sets)
('55ee66ff-7788-9900-aa11-cddeeff00112', 9, 4, '2024-05-02T09:20:00Z', 'e5a67891-0123-8f01-e567-901234ef0123', 60),

-- Leg Raise (goal: 15 reps, 3 sets)
('66ff7700-8899-aabb-bb22-deeff0011223', 14, 3, '2024-05-02T09:25:00Z', 'f6b78902-1234-9f12-f678-012345f01234', 0);

INSERT INTO t_e_session_performance_spe (spe_id, ses_id, spe_feeling, spe_date) VALUES
('91c091ea-3f8c-4d1d-9214-3a7643b9cb30', '1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'Felt strong throughout the session. Great pump.', '2024-06-01T08:00:00Z'),
('61a02f8b-61d7-4c91-8a91-133a53ad9f11', '7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'Good energy, but shoulder felt a bit tight.', '2024-07-02T08:00:00Z'),
('3d789b3a-d49f-4c9f-9a83-4f0df9d22977', 'f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'Tough leg day, but very satisfying.', '2024-05-06T08:00:00Z'),
('a6450ef0-c28b-4f53-b9e2-1c83d6d5e23a', '2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'Back felt a little sore, had to go lighter.', '2024-05-24T08:00:00Z'),
('b8cc110e-4aa1-4a9e-a6e1-79c48b69d4e5', '4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'Amazing hypertrophy session, incredible pump.', '2024-10-15T08:00:00Z'),
('18ecf789-4fbc-4ea9-8c7d-8d0aa733b5fd', 'ae5cb69b-2789-4c94-8ef8-9267ef034268', 'Glutes were burning! Core work was solid.', '2024-11-16T08:00:00Z'),
('b3d4c769-39d8-420e-8e2a-9f6db78e2a0a', '1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'Felt fatigued but managed to complete everything.', '2025-01-07T08:00:00Z'),
('cf789f13-ec2e-4d90-9ed0-8035a3e5aebb', '7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'Solid push session. Improved form on bench.', '2025-02-08T08:00:00Z'),
('ac55c96d-2f34-4ad1-8f19-98c1742ac1be', 'f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'Legs still sore from last time. Went lighter.', '2024-05-19T08:00:00Z'),
('df0b8a6f-64b9-4221-906c-b12c5c2fded2', '2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'Pull day felt great. Good grip strength today.', '2024-06-11T08:00:00Z'),
('20e041b2-09c3-4217-b8ce-5f54617090c1', '4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'Upper body on fire. Pumps were intense.', '2024-05-19T08:02:00Z'),
('5f7d22a9-c8df-4c42-9e7c-5bfb4a8be8f1', 'ae5cb69b-2789-4c94-8ef8-9267ef034268', 'Core exercises felt easier today. Big progress!', '2024-06-22T08:00:00Z'),
('c42c8fd4-979c-43d3-a7b6-32552c1c40b2', '1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'Better endurance this session. Happy overall.', '2024-05-23T08:00:00Z'),
('23b78677-1ed7-4cb3-9bb3-94750ef89e88', '7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'Shoulder still stiff. Skipped overhead press.', '2024-05-10T08:00:00Z'),
('b9b96fd4-7856-42f5-80ab-7a6a2b519f3c', 'f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'Perfect leg day. PR on squat!', '2024-05-15T10:00:00Z'),
('fedc2481-510b-4a60-b7d0-98fdef135c86', '2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'A little tired, but rows felt strong.', '2024-08-16T08:00:00Z');

INSERT INTO t_j_session_exercise_goal_seg (ses_id, exg_id) VALUES
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'a1c2345d-6789-4bcd-a123-567890abcdef'), -- Bench Press
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'c3e4567f-8901-6def-c345-789012cdef01'), -- Deadlift
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'd4f56780-9012-7ef0-d456-890123def012'), -- Squat
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'e5a67891-0123-8f01-e567-901234ef0123'), -- Military Press
('1bca7a9c-b7a4-44d8-97d3-5d8f82f8743c', 'f6b78902-1234-9f12-f678-012345f01234'), -- Leg Raise

('7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'a1c2345d-6789-4bcd-a123-567890abcdef'), -- Bench Press
('7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'b2d3456e-7890-5cde-b234-678901bcdef0'), -- Triceps Kickback
('7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'e5a67891-0123-8f01-e567-901234ef0123'), -- Military Press
('7351fd9b-6f6e-4bb5-b66a-73e180408d65', 'f6b78902-1234-9f12-f678-012345f01234'), -- Leg Raise

('f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'c3e4567f-8901-6def-c345-789012cdef01'), -- Deadlift
('f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'd4f56780-9012-7ef0-d456-890123def012'), -- Squat
('f3f1b471-7d1b-4c7d-89e1-7f03ff0d2e6c', 'f6b78902-1234-9f12-f678-012345f01234'), -- Leg Raise

('2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'c3e4567f-8901-6def-c345-789012cdef01'), -- Deadlift
('2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'b2d3456e-7890-5cde-b234-678901bcdef0'), -- Triceps Kickback (used as isolation pull)
('2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'a1c2345d-6789-4bcd-a123-567890abcdef'), -- Bench Press (optional compound)
('2e48b3ff-8fe1-4a8f-88b3-d13a0209debc', 'e5a67891-0123-8f01-e567-901234ef0123'), -- Military Press (upper back assist)

('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'a1c2345d-6789-4bcd-a123-567890abcdef'), -- Bench Press
('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'b2d3456e-7890-5cde-b234-678901bcdef0'), -- Triceps Kickback
('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'e5a67891-0123-8f01-e567-901234ef0123'), -- Military Press
('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'c3e4567f-8901-6def-c345-789012cdef01'), -- Deadlift
('4cd4b80a-25db-419d-b37a-45c3e3371a2c', 'd4f56780-9012-7ef0-d456-890123def012'), -- Squat

('ae5cb69b-2789-4c94-8ef8-9267ef034268', 'd4f56780-9012-7ef0-d456-890123def012'), -- Squat
('ae5cb69b-2789-4c94-8ef8-9267ef034268', 'f6b78902-1234-9f12-f678-012345f01234'), -- Leg Raise
('ae5cb69b-2789-4c94-8ef8-9267ef034268', 'c3e4567f-8901-6def-c345-789012cdef01'), -- Deadlift
('ae5cb69b-2789-4c94-8ef8-9267ef034268', 'b2d3456e-7890-5cde-b234-678901bcdef0'); -- Triceps Kickback (as accessory work)

INSERT INTO t_e_muscle_mus (mus_id, mus_name) VALUES
('b1a1c1d1-e1f1-41a1-91a1-11a1b1c1d1e1', 'Pectoralis Major'),
('b2a2c2d2-e2f2-42a2-92a2-22a2b2c2d2e2', 'Triceps Brachii'),
('b3a3c3d3-e3f3-43a3-93a3-33a3b3c3d3e3', 'Deltoid'),
('b4a4c4d4-e4f4-44a4-94a4-44a4b4c4d4e4', 'Latissimus Dorsi'),
('b5a5c5d5-e5f5-45a5-95a5-55a5b5c5d5e5', 'Biceps Brachii'),
('b6a6c6d6-e6f6-46a6-96a6-66a6b6c6d6e6', 'Quadriceps'),
('b7a7c7d7-e7f7-47a7-97a7-77a7b7c7d7e7', 'Hamstrings'),
('b8a8c8d8-e8f8-48a8-98a8-88a8b8c8d8e8', 'Gluteus Maximus'),
('b9a9c9d9-e9f9-49a9-99a9-99a9b9c9d9e9', 'Gastrocnemius'),
('c0a0c0d0-f0f0-40a0-90a0-00a0b0c0d0e0', 'Rectus Abdominis');

-- Bench Press: Pectoralis Major, Triceps Brachii, Deltoid
INSERT INTO t_e_exercise_muscle_link (exr_id, mus_id) VALUES
('9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', 'b1a1c1d1-e1f1-41a1-91a1-11a1b1c1d1e1'), -- Pectoralis Major
('9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', 'b2a2c2d2-e2f2-42a2-92a2-22a2b2c2d2e2'), -- Triceps Brachii
('9e3a1a58-fb71-4d6e-a3b1-68d1aefc7e01', 'b3a3c3d3-e3f3-43a3-93a3-33a3b3c3d3e3'), -- Deltoid

-- Deadlift: Hamstrings, Gluteus Maximus, Latissimus Dorsi
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', 'b7a7c7d7-e7f7-47a7-97a7-77a7b7c7d7e7'),
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', 'b8a8c8d8-e8f8-48a8-98a8-88a8b8c8d8e8'),
('6a345e31-2ba9-41ef-a23e-15cd9819fa89', 'b4a4c4d4-e4f4-44a4-94a4-44a4b4c4d4e4'),

-- Squat: Quadriceps, Gluteus Maximus, Hamstrings
('f89ea42b-56d2-4c36-8f4e-f76f8498f9df', 'b6a6c6d6-e6f6-46a6-96a6-66a6b6c6d6e6'),
('f89ea42b-56d2-4c36-8f4e-f76f8498f9df', 'b8a8c8d8-e8f8-48a8-98a8-88a8b8c8d8e8'),
('f89ea42b-56d2-4c36-8f4e-f76f8498f9df', 'b7a7c7d7-e7f7-47a7-97a7-77a7b7c7d7e7'),

-- Triceps Kickback: Triceps Brachii
('778899aa-bbcc-ddee-ff00-112233445566', 'b2a2c2d2-e2f2-42a2-92a2-22a2b2c2d2e2'),

-- Military Press: Deltoid, Triceps Brachii
('55667788-99aa-bbcc-ddee-ff0011223344', 'b3a3c3d3-e3f3-43a3-93a3-33a3b3c3d3e3'),
('55667788-99aa-bbcc-ddee-ff0011223344', 'b2a2c2d2-e2f2-42a2-92a2-22a2b2c2d2e2'),

-- Leg Raise: Abdominals
('eeff0011-2233-4455-6677-8899aabbccdd', 'c0a0c0d0-f0f0-40a0-90a0-00a0b0c0d0e0');