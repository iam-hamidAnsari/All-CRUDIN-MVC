1.Patient Management Models
public class Patient
{
    [Key]
    public int PatientID { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required]
    public DateTime DOB { get; set; }

    [Required, StringLength(10)]
    public string Gender { get; set; } // Use Enum for Gender

    [StringLength(15)]
    public string ContactInfo { get; set; }

    [StringLength(250)]
    public string Address { get; set; }

    [StringLength(15)]
    public string EmergencyContact { get; set; } // New field

    // Audit Fields
    public DateTime CreatedDate { get; set; }

    // Navigation Properties
    public MedicalHistory MedicalHistory { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; } // One-to-Many
}

public class AdmissionDischarge
{
    [Key]
    public int AdmissionDischargeID { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientID { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorID { get; set; }

    [Required]
    public DateTime AdmissionDate { get; set; }

    public DateTime? DischargeDate { get; set; } // Nullable for ongoing admissions

    [StringLength(250)]
    public string ReasonForAdmission { get; set; }

    [StringLength(50)]
    public string RoomNumber { get; set; }

    // Audit Fields
    public DateTime CreatedDate { get; set; }


    // Navigation Properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}

------------------------------------------------------------------------------------------------
2.Doctor and Staff Management Models
public class Doctor
{
    [Key]
    public int DoctorID { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Specialization { get; set; }

    [StringLength(15)]
    public string ContactInfo { get; set; }

    public int ExperienceYears { get; set; } // New field for experience

    // Audit Fields
    public DateTime CreatedDate { get; set; }

    // Navigation Properties
    public ICollection<Schedule> Schedules { get; set; } // One-to-Many
}

public class Staff
{
    [Key]
    public int StaffID { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required, StringLength(50)]
    public string Role { get; set; }

    [StringLength(15)]
    public string ContactInfo { get; set; }

    // Navigation Properties
    public ICollection<DutyRoster> DutyRosters { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
}

------------------------------------------------------------------------------------
3.Appointment and Scheduling Models

public class Appointment
{
    [Key]
    public int AppointmentID { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientID { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorID { get; set; }

    [ForeignKey(nameof(Schedule))]
    public int ScheduleID { get; set; }

    [Required]
    public string Status { get; set; } // Use Enum for Status


    // Navigation Properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public Schedule Schedule { get; set; }
}

public class Schedule
{
    [Key]
    public int ScheduleID { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorID { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    [StringLength(50)]
    public string AvailableDays { get; set; } // Replace with a dynamic schedule table for flexibility

    // Navigation Properties
    public Doctor Doctor { get; set; }
    public ICollection<Appointment> Appointments { get; set; } // One-to-Many
}

----------------------------------------------------------------------------------------------------------
4.Electronic Medical Records Models

public class MedicalHistory
{
    [Key]
    public int MedicalHistoryID { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientID { get; set; }

    [Required]
    public string Records { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation Property
    public Patient Patient { get; set; }
}

public class MedicalRecord
{
    [Key]
    public int RecordID { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientID { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorID { get; set; }

    [Required]
    public string Notes { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation Properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}

public class Prescription
{
    [Key]
    public int PrescriptionID { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientID { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorID { get; set; }

    [Required]
    public string MedicineDetails { get; set; } // Add fields for dosage, frequency, duration

    // Navigation Properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}

---------------------------------------------------------------------------------
5.Billing and Finance Management Models

public class Invoice
{
    [Key]
    public int InvoiceID { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientID { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    [Required]
    public string Status { get; set; } // Use Enum for Status

    public Patient Patient { get; set; }
}

public class Payment
{
    [Key]
    public int PaymentID { get; set; }

    [ForeignKey(nameof(Invoice))]
    public int InvoiceID { get; set; }

    [Required, StringLength(50)]
    public string PaymentType { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public Invoice Invoice { get; set; }
}

--------------------------------------------------------------------------
6.Pharmacy Management Models

public class PharmacyInventory
{
    [Key]
    public int DrugId { get; set; }

    [Required]
    public string DrugName { get; set; }

    public string Manufacturer { get; set; }
    public int StockQuantity { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Price { get; set; }
}

public class PharmacySale
{
    [Key]
    public int SaleId { get; set; }

    [ForeignKey(nameof(Prescription))]
    public int PrescriptionId { get; set; }

    [ForeignKey(nameof(PharmacyInventory))]
    public int DrugId { get; set; }

    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime SaleDate { get; set; }
    public Prescription Prescription { get; set; }
    public PharmacyInventory Drug { get; set; }
}
----------------------------------------------------------------------------------
7.Laboratory Management Models
public class Test
{
    [Key]
    public int TestId { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorID { get; set; }

    [Required]
    public string TestName { get; set; }

    public DateTime ScheduledDate { get; set; }

    [Required]
    public string SampleStatus { get; set; } // Use Enum for Status

    // Navigation Properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
}

public class TestResult
{
    [Key]
    public int ResultId { get; set; }

    [ForeignKey(nameof(Test))]
    public int TestId { get; set; }

    [Required]
    public string ResultDetails { get; set; }

    public DateTime ResultDate { get; set; }

    public Test Test { get; set; }
}





