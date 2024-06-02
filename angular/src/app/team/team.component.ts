import { TeamFilter } from './../proxy/teams/models';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TeamService, TeamDto, CreateUpdateTeamDto } from '@proxy/teams';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.scss'],
  providers: [ListService],
})
export class TeamComponent implements OnInit  {
  data = { items: [], totalCount: 0 } as PagedResultDto<TeamDto>;
  form: FormGroup;
  selected = {} as TeamDto;
  isModalOpen = false;
  filter: TeamFilter = {};
  filterHideShow = false;

  constructor(public readonly list: ListService,
    private service: TeamService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toast: ToasterService) {}

  ngOnInit() {
    this.getData();
  }

  getData(){
    const streamCreator = (query) => this.service.getListByFilter(query, this.filter);
    this.list.hookToQuery(streamCreator).subscribe((response) => {
      this.data = response;
    });
  }

  toggleFilter(){
    this.filterHideShow = !this.filterHideShow;
  }

  create() {
    this.selected = {} as TeamDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  edit(id: string) {
    this.service.get(id).subscribe((data) => {
      this.selected = data;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      			displayName: [this.selected.displayName, Validators.required],

    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    const dto: CreateUpdateTeamDto = {
      			displayName: this.form.get("displayName").value,

    };
    const request = this.selected.id
      ? this.service.update(this.selected.id, dto)
      : this.service.create(dto);

    request.subscribe(() => {
      this.toast.success(this.selected.id?'::TeamUpdated':'::TeamCreated', "::SUCCESS", {
        tapToDismiss: true,
        life: 2500
      });
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation.warn('::PressOKToContinue', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.service.delete(id).subscribe(() => {
          this.toast.success('::TeamDeleted', "::SUCCESS", {
            tapToDismiss: true,
            life: 2500
          });
          this.list.get();
        });
      }
    });
  }
}
