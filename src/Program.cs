using System.Globalization;

namespace SistemaAgendamento;

public class Program {
    private static readonly List<string> AnimaisNomes = [];
    private static readonly List<string> AnimaisEspecies = [];
    private static readonly List<int> AnimaisIdades = [];
    private static readonly List<string> AnimaisNomesTutores = [];
    private static readonly List<string> AnimaisTelefonesTutores = [];

    private static readonly List<string> VeterinariosNomes = [];
    private static readonly List<string> VeterinariosCrmvs = [];

    private static readonly List<int> ConsultasAnimaisIds = [];
    private static readonly List<int> ConsultasVeterinariosIds = [];
    private static readonly List<DateTime> ConsultasHorarios = [];
    private static readonly List<string> ConsultasSituacoes = [];
    private static readonly List<string> ConsultasObservacoes = [];

    private static void VisualizarLista<T>(List<T> lista) {
        for (int i=0; i<lista.Count; i++)
            Console.WriteLine($"    {i} || {lista[i]}");
        Console.WriteLine("==========================================");
    }
    public static bool ValidaHorario(DateTime horario) {
        if (
            horario.DayOfWeek == DayOfWeek.Saturday ||
            horario.DayOfWeek == DayOfWeek.Sunday ||
            horario.TimeOfDay < new TimeSpan(8,0,0) ||
            horario.TimeOfDay > new TimeSpan(17,30,0)
        )
            return false;

        TimeSpan meiaHora = new(0,30,0);
        foreach (DateTime horarioAgendado in ConsultasHorarios) if ((horario - horarioAgendado).Duration() < meiaHora)
            return false;

        return true;
    }

    private static void RegistrarAnimal() {
        Console.WriteLine("          REGISTRAR NOVO ANIMAL:");
        Console.WriteLine("==========================================");
        
        Console.Write("    Nome: ");
        AnimaisNomes.Add(Console.ReadLine() ?? "");
        
        Console.Write("    Espécie: ");
        AnimaisEspecies.Add(Console.ReadLine() ?? "");
        
        Console.Write("    Idade: ");
        bool idadeValida = int.TryParse(Console.ReadLine(),out int animalIdade);
        AnimaisIdades.Add(animalIdade);
        
        Console.Write("    Nome do tutor: ");
        AnimaisNomesTutores.Add(Console.ReadLine() ?? "");
        
        Console.Write("    Telefone do tutor: ");
        AnimaisTelefonesTutores.Add(Console.ReadLine() ?? "");
        
        Console.WriteLine("==========================================");
        Console.WriteLine("        REGISTRO FEITO COM SUCESSO");
    }
    private static void RegistrarVeterinario() {
        Console.WriteLine("          REGISTRAR VETERINÁRIO:");
        Console.WriteLine("==========================================");
        
        Console.Write("    Nome: ");
        VeterinariosNomes.Add(Console.ReadLine() ?? "");
        
        Console.Write("    CRMV: ");
        VeterinariosCrmvs.Add(Console.ReadLine() ?? "");
        
        Console.WriteLine("==========================================");
        Console.WriteLine("        REGISTRO FEITO COM SUCESSO");
    }
    private static void RegistrarConsulta() {
        Console.WriteLine("          REGISTRAR NOVA CONSULTA:");
        Console.WriteLine("==========================================");
        Console.WriteLine("    Formato do horário: dd/MM/yyyy HH:mm");
        Console.WriteLine("==========================================");
        
        VisualizarLista(AnimaisNomes);
        Console.Write("    ID do Animal: ");
        bool animalIdValido = int.TryParse(Console.ReadLine(),out int animalId);
        ConsultasAnimaisIds.Add(animalId);
        
        Console.WriteLine("==========================================");
        VisualizarLista(VeterinariosNomes);
        Console.Write("    ID do Veterinario: ");
        bool veterinarioIdValido = int.TryParse(Console.ReadLine(),out int veterinarioId);
        ConsultasVeterinariosIds.Add(veterinarioId);
        
        DateTime consultaHorario;
        do {
            Console.Write("    Horário da consulta: ");
            bool horarioValido = DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out consultaHorario
            );

            if (!ValidaHorario(consultaHorario)) {
                Console.WriteLine("==========================================");
                Console.WriteLine("          ERRO: HORÁRIO INVÁLIDO");
                Console.WriteLine("==========================================");
                continue;
            }

            ConsultasHorarios.Add(consultaHorario);
            break;
        } while (!ValidaHorario(consultaHorario));
        
        ConsultasSituacoes.Add("agendada");
        ConsultasObservacoes.Add("");
        
        Console.WriteLine("==========================================");
        Console.WriteLine("        REGISTRO FEITO COM SUCESSO");
    }
    private static void VisualizarConsultas() {
        for (int i=0; i<ConsultasHorarios.Count; i++) {
            int aI = ConsultasAnimaisIds[i];
            int vI = ConsultasVeterinariosIds[i];

            Console.WriteLine($"{ConsultasHorarios[i]:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"    {AnimaisEspecies[aI],4} || {AnimaisNomes[aI]}");
            Console.WriteLine($"    Vet. || {VeterinariosNomes[vI]}");
            Console.WriteLine($"    Sit. || {ConsultasSituacoes[i]}");
            Console.WriteLine($"    Obs. || {ConsultasObservacoes[i]}");
            Console.WriteLine("==========================================");
        }
    }

    public static void Main() {
        Console.WriteLine("==========================================");
        Console.WriteLine("========= SISTEMA DE AGENDAMENTO =========");
        
        byte escolha;
        do {
            Console.WriteLine("==========================================");
            Console.WriteLine("=========================================="); 
            Console.WriteLine("         Escolha a opção desejada:");
            Console.WriteLine("==========================================");
            Console.WriteLine("    1 || Registrar novo animal");
            Console.WriteLine("    2 || Registrar veterinário");
            Console.WriteLine("    3 || Registrar nova consulta");
            Console.WriteLine("    4 || Visualizar a lista de consultas");
            Console.WriteLine("    0 || SAIR");
            
            Console.WriteLine("==========================================");
            Console.Write("    ESCOLHA: ");
            bool escolhaValida = byte.TryParse(Console.ReadLine(),out escolha);

            Console.WriteLine("==========================================");
            switch (escolha) {
                case 1:
                    RegistrarAnimal();
                    break;
                case 2:
                    RegistrarVeterinario();
                    break;
                case 3:
                    RegistrarConsulta();
                    break;
                case 4:
                    VisualizarConsultas();
                    break;
                case 0:
                    Console.WriteLine("=========== PROGRAMA ENCERRADO ===========");
                    Console.WriteLine("==========================================");
                    break;
                default:
                    Console.WriteLine("           ERRO: OPÇÃO INVÁLIDA");
                    break;
            }
        } while (escolha != 0);
    }
}
