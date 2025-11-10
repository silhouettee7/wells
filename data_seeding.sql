insert into measurement_types (name, unit, description) values
    ('bottom_hole_pressure', 'mpa', 'забойное давление'),
    ('reservoir_pressure', 'mpa', 'пластовое давление'),
    ('pump_intake_pressure', 'mpa', 'давление на приеме насоса');

insert into departments (name) values
    ('управление №1'),
    ('управление №2'),
    ('управление №3');

insert into wells (name, department_id, commission_date) values
    ('скважина_101', 1, '2020-01-15'),
    ('скважина_102', 1, '2020-03-20'),
    ('скважина_103', 1, '2020-05-10'),
    ('скважина_104', 1, '2020-07-25'),
    ('скважина_105', 1, '2020-09-30'),

    ('скважина_201', 2, '2019-11-05'),
    ('скважина_202', 2, '2020-02-14'),
    ('скважина_203', 2, '2020-04-18'),
    ('скважина_204', 2, '2020-06-22'),
    ('скважина_205', 2, '2020-08-11'),

    ('скважина_301', 3, '2018-12-10'),
    ('скважина_302', 3, '2019-03-15'),
    ('скважина_303', 3, '2019-06-20'),
    ('скважина_304', 3, '2019-09-25'),
    ('скважина_305', 3, '2020-01-08');

create or replace function generate_test_measurements()
returns void as $$
declare
    well_record record;
    measurement_type_record record;
    curr_time timestamp;
    day_counter integer;
    minute_counter integer;
    base_pressure decimal(10,4);
    pressure_variation decimal(10,4);
    start_date timestamp;
begin
    start_date := '2025-01-01 00:00:00'::timestamp;
    for well_record in select id from wells loop
        for measurement_type_record in select id from measurement_types loop

            case measurement_type_record.id
                when 1 then base_pressure := 25.0;
                when 2 then base_pressure := 30.0;
                when 3 then base_pressure := 15.0;
            end case;

            for day_counter in 0..4 loop
                for minute_counter in 0..1439 loop
                    curr_time :=  start_date + (interval '1 day' * day_counter)
                                   + (interval '1 minute' * minute_counter);

                    pressure_variation := base_pressure * (0.9 + random() * 0.2);

                    insert into measurements (well_id, measurement_type_id, value, measurement_time)
                    values (
                        well_record.id,
                        measurement_type_record.id,
                        round(pressure_variation::numeric, 4),
                        curr_time
                    );
                end loop;
            end loop;
        end loop;
    end loop;
end;
$$ language plpgsql;

select generate_test_measurements();

drop function generate_test_measurements();